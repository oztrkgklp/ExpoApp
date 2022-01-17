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
    public class DeleteExternalAttendanceByIdCommandHandler : IRequestHandler<DeleteExternalAttendanceByIdCommand, DeleteExternalAttendanceByIdCommandResult>
    {
        private readonly ILogger<DeleteExternalAttendanceByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeleteExternalAttendanceByIdCommandHandler(ILogger<DeleteExternalAttendanceByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeleteExternalAttendanceByIdCommandResult> Handle(DeleteExternalAttendanceByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteExternalAttendanceById = await _expoAPIQueryRepository.DeleteExternalAttendanceByIdAsync(command.ExternalAttendanceID, cancellationToken);

                _logger.LogInformation("The external attendance has been deleted.");

                return new DeleteExternalAttendanceByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/external-attendances/{command.ExternalAttendanceID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new DeleteExternalAttendanceByIdCommandResult()
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
                    ReturnPath = $"/ExternalAttendances/{command.ExternalAttendanceID}"
                };
            }
        }
    }
}
