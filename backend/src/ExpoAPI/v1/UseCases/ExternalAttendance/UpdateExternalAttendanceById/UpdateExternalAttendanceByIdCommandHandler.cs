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
    public class UpdateExternalAttendanceByIdCommandHandler : IRequestHandler<UpdateExternalAttendanceByIdCommand, UpdateExternalAttendanceByIdCommandResult>
    {
        private readonly ILogger<UpdateExternalAttendanceByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateExternalAttendanceByIdCommandHandler(ILogger<UpdateExternalAttendanceByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateExternalAttendanceByIdCommandResult> Handle(UpdateExternalAttendanceByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateExternalAttendanceById = await _expoAPIQueryRepository.UpdateExternalAttendanceByIdAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The external attendance has been updated.");

                return new UpdateExternalAttendanceByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/external-attendances/{command.Contract.ExternalAttendanceID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new UpdateExternalAttendanceByIdCommandResult()
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
                    ReturnPath = $"/external-attendances/{command.Contract.ExternalAttendanceID}"
                };
            }
        }
    }
}
