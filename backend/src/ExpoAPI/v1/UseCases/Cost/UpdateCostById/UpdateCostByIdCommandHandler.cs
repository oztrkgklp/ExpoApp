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
    public class UpdateCostByIdCommandHandler : IRequestHandler<UpdateCostByIdCommand, UpdateCostByIdCommandResult>
    {
        private readonly ILogger<UpdateCostByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateCostByIdCommandHandler(ILogger<UpdateCostByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateCostByIdCommandResult> Handle(UpdateCostByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateCostById = await _expoAPIQueryRepository.UpdateCostByIdAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The cost has been updated.");

                return new UpdateCostByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/costs/{command.Contract.CostID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new UpdateCostByIdCommandResult()
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
                    ReturnPath = $"/costs/{command.Contract.CostID}"
                };
            }
        }
    }
}
