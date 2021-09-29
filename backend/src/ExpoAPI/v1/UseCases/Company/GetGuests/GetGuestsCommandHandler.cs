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
    public class GetGuestsCommandHandler : IRequestHandler<GetGuestsCommand, GetGuestsCommandResult>
    {
        private readonly ILogger<GetGuestsCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetGuestsCommandHandler(ILogger<GetGuestsCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetGuestsCommandResult> Handle(GetGuestsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getGuests = await _expoAPIQueryRepository.GetGuestsAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetGuestsCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContracts = getGuests.ToList(),
                    ReturnPath = $"/guests"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetGuestsCommandResult()
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
                    ReturnPath = $"/companies"
                };
            }
        }
    }
}
