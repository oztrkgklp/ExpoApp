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
    public class DeleteExpenseByIdCommandHandler : IRequestHandler<DeleteExpenseByIdCommand, DeleteExpenseByIdCommandResult>
    {
        private readonly ILogger<DeleteExpenseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeleteExpenseByIdCommandHandler(ILogger<DeleteExpenseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeleteExpenseByIdCommandResult> Handle(DeleteExpenseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteExpenseById = await _expoAPIQueryRepository.DeleteExpenseByIdAsync(command.ExpenseID, cancellationToken);

                _logger.LogInformation("The expense has been deleted.");

                return new DeleteExpenseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/expenses/{command.ExpenseID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new DeleteExpenseByIdCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10003,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10003),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/expenses/{command.ExpenseID}"
                };
            }
        }
    }
}
