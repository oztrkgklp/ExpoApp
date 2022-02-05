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
    public class GetCostByIdCommandHandler : IRequestHandler<GetCostByIdCommand, GetCostByIdCommandResult>
    {
        private readonly ILogger<GetCostByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCostByIdCommandHandler(ILogger<GetCostByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCostByIdCommandResult> Handle(GetCostByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCostById = await _expoAPIQueryRepository.GetCostByIdAsync(command.CostId,cancellationToken);

                _logger.LogInformation("The cost with id {@CostId} has been deleted.",command.CostId);


                return new GetCostByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CostContract = getCostById,
                    ReturnPath = $"/costs/{command.CostId}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.CostId);

                return new GetCostByIdCommandResult()
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
                    ReturnPath = $"/costs/{command.CostId}"
                };
            }
        }
    }
}
