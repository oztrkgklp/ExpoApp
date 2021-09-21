using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Timeout;

namespace ExpoAPI.Infrastructure.Repositories
{
    public class DapperPolly : IDapperPolly
    {
        private const int RetryCount = 5;
        private const int ExceptionsAllowedBeforeBreaking = 3;
        private List<IAsyncPolicy> _policies;

        private static readonly int[] SqlTransientErrors =
        {
            (int) SqlHandledExceptions.DatabaseNotCurrentlyAvailable,
            (int) SqlHandledExceptions.ErrorProcessingRequest,
            (int) SqlHandledExceptions.ServiceCurrentlyBusy,
            (int) SqlHandledExceptions.NotEnoughResources
        };

        private static ILogger<DapperPolly>? _logger;

        public DapperPolly(ILogger<DapperPolly>? logger)
        {
            _logger = logger;
            _policies = new List<IAsyncPolicy>();
            _policies.Add(GetTimeOutPolicy(GetTimeout(), "SqlOverallTimeoutAsyncPolicy"));
            _policies.Add(GetCommonTransientErrorsPolicies(RetryCount, "SqlCommonTransientErrorsAsyncPolicy"));
            _policies.AddRange(GetCircuitBreakerPolicies(ExceptionsAllowedBeforeBreaking));
        }


        /// <summary>
        /// https://github.com/App-vNext/Polly/issues/107
        /// https://github.com/App-vNext/Polly/wiki/PolicyWrap
        /// todo:read from config and move to the noc fx
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsyncWithRetry<T>(IDbConnection cnn, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var policyWrap = Policy.WrapAsync(_policies.ToArray());
            return await policyWrap.ExecuteAsync(async () => await cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType));
        }

        private static TimeSpan GetTimeout()
        {
            var retry = 1;
            var delay = TimeSpan.Zero;
            while (retry <= RetryCount)
            {
                delay += TimeSpan.FromSeconds(Math.Pow(2, retry));
                retry++;
            }

            // plus an arbitrary max time the operation could take
            return delay + TimeSpan.FromSeconds(10);
        }

        private static IAsyncPolicy GetTimeOutPolicy(TimeSpan timeout, string policyName) =>
            Policy
                .TimeoutAsync(
                    timeout,
                    TimeoutStrategy.Pessimistic)
                .WithPolicyKey(policyName);

        private static IAsyncPolicy GetCommonTransientErrorsPolicies(int retryCount, string policyName) =>
            Policy
                .Handle<SqlException>(ex => SqlTransientErrors.Contains(ex.Number))
                .WaitAndRetryAsync(
                    // number of retries
                    retryCount,
                    // exponential back-off
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    // on retry
                    (exception, timeSpan, retries, context) =>
                    {
                        _logger.LogError(
                            exception,
                            "ERROR: Error talking to Db, will retry after {RetryTimeSpan}. Retry attempt {RetryCount}",
                            timeSpan,
                            retryCount);
                    })
                .WithPolicyKey(policyName);

        private static IAsyncPolicy[] GetCircuitBreakerPolicies(int exceptionsAllowedBeforeBreaking)
            => new IAsyncPolicy[]
            {
                Policy
                    .Handle<SqlException>(ex => ex.Number == (int) SqlHandledExceptions.DatabaseNotCurrentlyAvailable)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F1.SqlCircuitBreakerAsyncPolicy"),
                Policy
                    .Handle<SqlException>(ex => ex.Number == (int) SqlHandledExceptions.ErrorProcessingRequest)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F2.SqlCircuitBreakerAsyncPolicy"),
                Policy
                    .Handle<SqlException>(ex => ex.Number == (int) SqlHandledExceptions.ServiceCurrentlyBusy)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F3.SqlCircuitBreakerAsyncPolicy"),
                Policy
                    .Handle<SqlException>(ex => ex.Number == (int) SqlHandledExceptions.NotEnoughResources)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F4.SqlCircuitBreakerAsyncPolicy"),
                Policy
                    .Handle<SqlException>(
                        ex => ex.Number == (int) SqlHandledExceptions.SessionTerminatedLongTransaction)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F5.SqlCircuitBreakerAsyncPolicy"),
                Policy
                    .Handle<SqlException>(ex => ex.Number == (int) SqlHandledExceptions.SessionTerminatedToManyLocks)
                    .CircuitBreakerAsync(
                        // number of exceptions before breaking circuit
                        exceptionsAllowedBeforeBreaking,
                        // time circuit opened before retry
                        TimeSpan.FromSeconds(30),
                        OnBreak,
                        OnReset,
                        OnHalfOpen)
                    .WithPolicyKey($"F6.SqlCircuitBreakerAsyncPolicy")
            };

        private static void OnHalfOpen() => _logger.LogWarning("#Polly #CircuitBreakerAsync Half-open: Next call is a trial");

        private static void OnReset() =>
            // on circuit closed
            _logger.LogWarning("#Polly #CircuitBreakerAsync Circuit breaker reset");

        private static void OnBreak(Exception exception, TimeSpan duration) =>
            // on circuit opened
            _logger.LogWarning("#Polly #CircuitBreakerAsync Circuit breaker opened", exception);
    }

    internal enum SqlHandledExceptions
    {
        DatabaseNotCurrentlyAvailable = 40613,
        ErrorProcessingRequest = 40197,
        ServiceCurrentlyBusy = 40501,
        NotEnoughResources = 49918,
        SessionTerminatedLongTransaction = 40549,
        SessionTerminatedToManyLocks = 40550
    }
}