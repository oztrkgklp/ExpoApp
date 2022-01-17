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
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, CreateExpenseCommandResult>
    {
        private readonly ILogger<CreateExpenseCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreateExpenseCommandHandler(ILogger<CreateExpenseCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreateExpenseCommandResult> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createExpense = await _expoAPIQueryRepository.CreateExpenseAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The expense has been created.");

                return new CreateExpenseCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/expenses"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new CreateExpenseCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10002,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10002),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/expenses"
                };
            }
        }
    }
}
