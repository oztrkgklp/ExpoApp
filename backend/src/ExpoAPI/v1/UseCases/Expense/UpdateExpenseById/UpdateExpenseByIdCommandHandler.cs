using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using System;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.UseCases.Expense
{
    public class UpdateExpenseByIdCommandHandler : IRequestHandler<UpdateExpenseByIdCommand, UpdateExpenseByIdCommandResult>
    {
        private readonly ILogger<UpdateExpenseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateExpenseByIdCommandHandler(ILogger<UpdateExpenseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateExpenseByIdCommandResult> Handle(UpdateExpenseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateExpenseById = await _expoAPIQueryRepository.UpdateExpenseByIdAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The expense has been updated.");

                return new UpdateExpenseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/expenses/{command.Contract.ExpenseID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new UpdateExpenseByIdCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10001,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10001),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/expenses/{command.Contract.ExpenseID}"
                };
            }
        }
    }
}
