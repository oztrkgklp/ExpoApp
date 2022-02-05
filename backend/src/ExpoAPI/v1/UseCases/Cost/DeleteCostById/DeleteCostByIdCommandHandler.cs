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

namespace ExpoAPI.UseCases.Cost
{
    public class DeleteCostByIdCommandHandler : IRequestHandler<DeleteCostByIdCommand, DeleteCostByIdCommandResult>
    {
        private readonly ILogger<DeleteCostByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeleteCostByIdCommandHandler(ILogger<DeleteCostByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeleteCostByIdCommandResult> Handle(DeleteCostByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteCostById = await _expoAPIQueryRepository.DeleteCostByIdAsync(command.CostID, cancellationToken);

                _logger.LogInformation("The cost has been deleted.");

                return new DeleteCostByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/costs/{command.CostID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new DeleteCostByIdCommandResult()
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
                    ReturnPath = $"/costs/{command.CostID}"
                };
            }
        }
    }
}
