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

namespace ExpoAPI.UseCases.Accommodation
{
    public class UpdateAccommodationByIdCommandHandler : IRequestHandler<UpdateAccommodationByIdCommand, UpdateAccommodationByIdCommandResult>
    {
        private readonly ILogger<UpdateAccommodationByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateAccommodationByIdCommandHandler(ILogger<UpdateAccommodationByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateAccommodationByIdCommandResult> Handle(UpdateAccommodationByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateAccommodationById = await _expoAPIQueryRepository.UpdateAccommodationByIdAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The accommodation has been updated.");

                return new UpdateAccommodationByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/accommodations/{command.Contract.AccommodationID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new UpdateAccommodationByIdCommandResult()
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
                    ReturnPath = $"/accommodations/{command.Contract.AccommodationID}"
                };
            }
        }
    }
}
