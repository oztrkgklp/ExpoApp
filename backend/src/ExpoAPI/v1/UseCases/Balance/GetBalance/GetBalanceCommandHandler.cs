using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.UseCases.Balance
{
    public class GetBalanceCommandHandler : IRequestHandler<GetBalanceCommand, GetBalanceCommandResult>
    {
        private readonly ILogger<GetBalanceCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetBalanceCommandHandler(ILogger<GetBalanceCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetBalanceCommandResult> Handle(GetBalanceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getBalance = await _expoAPIQueryRepository.GetBalanceAsync(cancellationToken);

                if (getBalance == null)
                {
                    _logger.LogError("Could not fetch the balance");

                    return new GetBalanceCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Balance not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/balance"
                    };
                }

                _logger.LogInformation("The balance has been fetched.");

                return new GetBalanceCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    BalanceContract = new BalanceContract()
                    {
                        BalanceID = getBalance.BalanceID,
                        Amount = getBalance.Amount,
                    },
                    ReturnPath = $"/balance"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message}", ex.Message);

                return new GetBalanceCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10000,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/balance"
                };
            }
        }
    }
}
