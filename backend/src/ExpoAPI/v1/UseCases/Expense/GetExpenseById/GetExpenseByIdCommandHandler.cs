using System;
using System.Collections.Generic;
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
    public class GetExpenseByIdCommandHandler : IRequestHandler<GetExpenseByIdCommand, GetExpenseByIdCommandResult>
    {
        private readonly ILogger<GetExpenseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetExpenseByIdCommandHandler(ILogger<GetExpenseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetExpenseByIdCommandResult> Handle(GetExpenseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getExpenseById = await _expoAPIQueryRepository.GetExpenseByIdAsync(command.ExpenseId,cancellationToken);

                _logger.LogInformation("The expense with id {@expenseId} has been deleted.",command.ExpenseId);


                return new GetExpenseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ExpenseContract = getExpenseById,
                    ReturnPath = $"/expenses/{command.ExpenseId}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.ExpenseId);

                return new GetExpenseByIdCommandResult()
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
                    ReturnPath = $"/expenses/{command.ExpenseId}"
                };
            }
        }
    }
}
