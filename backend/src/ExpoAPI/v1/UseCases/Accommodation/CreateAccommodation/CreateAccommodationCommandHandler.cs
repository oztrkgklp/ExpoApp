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
    public class CreateAccommodationCommandHandler : IRequestHandler<CreateAccommodationCommand, CreateAccommodationCommandResult>
    {
        private readonly ILogger<CreateAccommodationCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreateAccommodationCommandHandler(ILogger<CreateAccommodationCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreateAccommodationCommandResult> Handle(CreateAccommodationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createAccommodation = await _expoAPIQueryRepository.CreateAccommodationAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The accommodation has been created.");

                return new CreateAccommodationCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/accommodations"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new CreateAccommodationCommandResult()
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
                    ReturnPath = $"/accommodations"
                };
            }
        }
    }
}
