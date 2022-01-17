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

namespace ExpoAPI.UseCases.ExternalAttendance
{
    public class GetExternalAttendancesCommandHandler : IRequestHandler<GetExternalAttendancesCommand, GetExternalAttendancesCommandResult>
    {
        private readonly ILogger<GetExternalAttendancesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetExternalAttendancesCommandHandler(ILogger<GetExternalAttendancesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetExternalAttendancesCommandResult> Handle(GetExternalAttendancesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getExternalAttendances = await _expoAPIQueryRepository.GetExternalAttendancesAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetExternalAttendancesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ExternalAttendanceContracts = getExternalAttendances.ToList(),
                    ReturnPath = $"/external-attendances"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetExternalAttendancesCommandResult()
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
                    ReturnPath = $"/external-attendances"
                };
            }
        }
    }
}
