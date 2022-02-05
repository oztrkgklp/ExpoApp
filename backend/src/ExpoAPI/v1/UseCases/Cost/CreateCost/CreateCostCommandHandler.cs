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

namespace ExpoAPI.UseCases.Cost
{
    public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, CreateCostCommandResult>
    {
        private readonly ILogger<CreateCostCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreateCostCommandHandler(ILogger<CreateCostCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreateCostCommandResult> Handle(CreateCostCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createCost = await _expoAPIQueryRepository.CreateCostAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The cost has been created.");

                return new CreateCostCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/costs"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new CreateCostCommandResult()
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
                    ReturnPath = $"/costs"
                };
            }
        }
    }
}
