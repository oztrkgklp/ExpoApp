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

namespace ExpoAPI.UseCases.OtelInformation
{
    public class UpdateOtelInformationCommandHandler : IRequestHandler<UpdateOtelInformationCommand, UpdateOtelInformationCommandResult>
    {
        private readonly ILogger<UpdateOtelInformationCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateOtelInformationCommandHandler(ILogger<UpdateOtelInformationCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateOtelInformationCommandResult> Handle(UpdateOtelInformationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateOtelInformation = await _expoAPIQueryRepository.UpdateOtelInformationAsync(command.OtelInformationContract,cancellationToken);

                _logger.LogInformation("The company with id {@companyId} has been updated.",command.OtelInformationContract.OtelInformationID);

                return new UpdateOtelInformationCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/otel-info/{command.OtelInformationContract.OtelInformationID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.OtelInformationContract.OtelInformationID);

                return new UpdateOtelInformationCommandResult()
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
                    ReturnPath = $"/otel-info/{command.OtelInformationContract.OtelInformationID}"
                };
            }
        }
    }
}
