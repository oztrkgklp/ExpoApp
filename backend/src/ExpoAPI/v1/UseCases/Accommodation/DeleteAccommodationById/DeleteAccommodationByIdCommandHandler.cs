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

namespace ExpoAPI.UseCases.Accommodation
{
    public class DeleteAccommodationByIdCommandHandler : IRequestHandler<DeleteAccommodationByIdCommand, DeleteAccommodationByIdCommandResult>
    {
        private readonly ILogger<DeleteAccommodationByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeleteAccommodationByIdCommandHandler(ILogger<DeleteAccommodationByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeleteAccommodationByIdCommandResult> Handle(DeleteAccommodationByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteAccommodationById = await _expoAPIQueryRepository.DeleteAccommodationByIdAsync(command.AccommodationID, cancellationToken);

                _logger.LogInformation("The accommodation has been deleted.");

                return new DeleteAccommodationByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/accommodations/{command.AccommodationID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new DeleteAccommodationByIdCommandResult()
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
                    ReturnPath = $"/accommodations/{command.AccommodationID}"
                };
            }
        }
    }
}
