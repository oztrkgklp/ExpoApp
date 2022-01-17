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

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class GetExternalAttendanceByIdCommandHandler : IRequestHandler<GetExternalAttendanceByIdCommand, GetExternalAttendanceByIdCommandResult>
    {
        private readonly ILogger<GetExternalAttendanceByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetExternalAttendanceByIdCommandHandler(ILogger<GetExternalAttendanceByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetExternalAttendanceByIdCommandResult> Handle(GetExternalAttendanceByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getExternalAttendanceById = await _expoAPIQueryRepository.GetExternalAttendanceByIdAsync(command.ExternalAttendanceId,cancellationToken);

                _logger.LogInformation("The external attendance with id {@ExternalAttendanceId} has been deleted.",command.ExternalAttendanceId);


                return new GetExternalAttendanceByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ExternalAttendanceContract = getExternalAttendanceById,
                    ReturnPath = $"/external-attendances/{command.ExternalAttendanceId}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.ExternalAttendanceId);

                return new GetExternalAttendanceByIdCommandResult()
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
                    ReturnPath = $"/external-attendances/{command.ExternalAttendanceId}"
                };
            }
        }
    }
}
