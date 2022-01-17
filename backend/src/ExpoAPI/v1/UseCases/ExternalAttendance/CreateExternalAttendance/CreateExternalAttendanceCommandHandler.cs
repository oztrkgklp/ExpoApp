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

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class CreateExternalAttendanceCommandHandler : IRequestHandler<CreateExternalAttendanceCommand, CreateExternalAttendanceCommandResult>
    {
        private readonly ILogger<CreateExternalAttendanceCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreateExternalAttendanceCommandHandler(ILogger<CreateExternalAttendanceCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreateExternalAttendanceCommandResult> Handle(CreateExternalAttendanceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createExternalAttendance = await _expoAPIQueryRepository.CreateExternalAttendanceAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The external attendance has been created.");

                return new CreateExternalAttendanceCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/external-attendances"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new CreateExternalAttendanceCommandResult()
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
                    ReturnPath = $"/external-attendances"
                };
            }
        }
    }
}
