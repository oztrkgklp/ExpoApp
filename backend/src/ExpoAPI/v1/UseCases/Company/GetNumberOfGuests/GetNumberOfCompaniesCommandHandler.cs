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

namespace ExpoAPI.UseCases.Company
{
    public class GetNumberOfGuestsCommandHandler : IRequestHandler<GetNumberOfGuestsCommand, GetNumberOfGuestsCommandResult>
    {
        private readonly ILogger<GetNumberOfGuestsCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNumberOfGuestsCommandHandler(ILogger<GetNumberOfGuestsCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNumberOfGuestsCommandResult> Handle(GetNumberOfGuestsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getNumberOfGuests = await _expoAPIQueryRepository.GetNumberOfGuestsAsync(cancellationToken);

                _logger.LogInformation("The number of companies information has been fetched.");

                return new GetNumberOfGuestsCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    NumberOfGuests = getNumberOfGuests,
                    ReturnPath = $"/guests/count"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNumberOfGuestsCommandResult()
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
                    ReturnPath = $"/guests/count"
                };
            }
        }
    }
}
