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
    public class GetEnteredCompaniesWithoutPurchaseCommandHandler : IRequestHandler<GetEnteredCompaniesWithoutPurchaseCommand, GetEnteredCompaniesWithoutPurchaseCommandResult>
    {
        private readonly ILogger<GetEnteredCompaniesWithoutPurchaseCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetEnteredCompaniesWithoutPurchaseCommandHandler(ILogger<GetEnteredCompaniesWithoutPurchaseCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetEnteredCompaniesWithoutPurchaseCommandResult> Handle(GetEnteredCompaniesWithoutPurchaseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanies = await _expoAPIQueryRepository.GetEnteredCompaniesWithoutPurchaseAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetEnteredCompaniesWithoutPurchaseCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContracts = getCompanies.ToList(),
                    ReturnPath = $"/companies/entered/no-purchase"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetEnteredCompaniesWithoutPurchaseCommandResult()
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
                    ReturnPath = $"/companies/entered/no-purchase"
                };
            }
        }
    }
}
