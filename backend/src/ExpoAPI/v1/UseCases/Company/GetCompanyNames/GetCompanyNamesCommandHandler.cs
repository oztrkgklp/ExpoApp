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
    public class GetCompanyNamesCommandHandler : IRequestHandler<GetCompanyNamesCommand, GetCompanyNamesCommandResult>
    {
        private readonly ILogger<GetCompanyNamesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCompanyNamesCommandHandler(ILogger<GetCompanyNamesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCompanyNamesCommandResult> Handle(GetCompanyNamesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanyNames = await _expoAPIQueryRepository.GetCompanyNamesAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetCompanyNamesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyNames = getCompanyNames.ToList(),
                    ReturnPath = $"/company-names"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetCompanyNamesCommandResult()
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
                    ReturnPath = $"/company-names"
                };
            }
        }
    }
}
