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

namespace ExpoAPI.UseCases.Company
{
    public class GetTotalEndorsementCommandHandler : IRequestHandler<GetTotalEndorsementCommand, GetTotalEndorsementCommandResult>
    {
        private readonly ILogger<GetTotalEndorsementCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetTotalEndorsementCommandHandler(ILogger<GetTotalEndorsementCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetTotalEndorsementCommandResult> Handle(GetTotalEndorsementCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getTotalEndorsement = await _expoAPIQueryRepository.GetTotalEndorsementAsync(cancellationToken);

                _logger.LogInformation("The total endorsement has been fetched.");

                return new GetTotalEndorsementCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    TotalEndorsement = getTotalEndorsement,
                    ReturnPath = $"/companies/endorsement"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetTotalEndorsementCommandResult()
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
                    ReturnPath = $"/companies/endorsement"
                };
            }
        }
    }
}
