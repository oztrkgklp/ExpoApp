using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExpoAPI.UseCases.OtelInformation
{
    public class GetOtelInformationCommandHandler : IRequestHandler<GetOtelInformationCommand, GetOtelInformationCommandResult>
    {
        private readonly ILogger<GetOtelInformationCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetOtelInformationCommandHandler(ILogger<GetOtelInformationCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetOtelInformationCommandResult> Handle(GetOtelInformationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getOtelInformation = await _expoAPIQueryRepository.GetOtelInformationAsync(cancellationToken);

                if (getOtelInformation == null)
                {
                    _logger.LogError("Could not fetch the otel information");

                    return new GetOtelInformationCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Otel info not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/otel"
                    };
                }

                _logger.LogInformation("The otel information has been fetched.");

                return new GetOtelInformationCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    OtelInformationContract = new OtelInformationContract()
                    {
                        OtelInformationID = getOtelInformation.OtelInformationID,
                        SNG = getOtelInformation.SNG,
                        DBL = getOtelInformation.DBL,
                        TRPL = getOtelInformation.TRPL,
                        QUAT = getOtelInformation.QUAT,
                        SNGCHD = getOtelInformation.SNGCHD,
                        DBLCHD = getOtelInformation.DBLCHD,
                        TRPLCHD = getOtelInformation.TRPLCHD
                    },
                    ReturnPath = $"/otel-info"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message}", ex.Message);

                return new GetOtelInformationCommandResult()
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
                    ReturnPath = $"/otel-info"
                };
            }
        }
    }
}
