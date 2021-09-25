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
    public class GetNotEnteredCompaniesCommandHandler : IRequestHandler<GetNotEnteredCompaniesCommand, GetNotEnteredCompaniesCommandResult>
    {
        private readonly ILogger<GetNotEnteredCompaniesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNotEnteredCompaniesCommandHandler(ILogger<GetNotEnteredCompaniesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNotEnteredCompaniesCommandResult> Handle(GetNotEnteredCompaniesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanies = await _expoAPIQueryRepository.GetNotEnteredCompaniesAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetNotEnteredCompaniesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContracts = getCompanies.ToList(),
                    ReturnPath = $"/companies"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNotEnteredCompaniesCommandResult()
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
