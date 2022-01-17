using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.UseCases.Expense
{
    public class GetExpensesCommandHandler : IRequestHandler<GetExpensesCommand, GetExpensesCommandResult>
    {
        private readonly ILogger<GetExpensesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetExpensesCommandHandler(ILogger<GetExpensesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetExpensesCommandResult> Handle(GetExpensesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getExpenses = await _expoAPIQueryRepository.GetExpensesAsync(cancellationToken);

                if (getExpenses == null)
                {
                    _logger.LogError("Could not fetch the expenses");

                    return new GetExpensesCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Expenses not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/expenses"
                    };
                }

                _logger.LogInformation("The expense data has been fetched.");

                return new GetExpensesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ExpenseContracts = getExpenses.ToList(),
                    ReturnPath = $"/expenses"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetExpensesCommandResult()
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
                    ReturnPath = $"/expenses"
                };
            }
        }
    }
}
