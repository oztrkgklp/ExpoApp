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

namespace ExpoAPI.UseCases.Cost
{
    public class GetCostsCommandHandler : IRequestHandler<GetCostsCommand, GetCostsCommandResult>
    {
        private readonly ILogger<GetCostsCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCostsCommandHandler(ILogger<GetCostsCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCostsCommandResult> Handle(GetCostsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCosts = await _expoAPIQueryRepository.GetCostsAsync(cancellationToken);

                if (getCosts == null)
                {
                    _logger.LogError("Could not fetch the costs");

                    return new GetCostsCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Costs not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/costs"
                    };
                }

                _logger.LogInformation("The cost data has been fetched.");

                return new GetCostsCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CostContracts = getCosts.ToList(),
                    ReturnPath = $"/costs"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetCostsCommandResult()
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
                    ReturnPath = $"/costs"
                };
            }
        }
    }
}
