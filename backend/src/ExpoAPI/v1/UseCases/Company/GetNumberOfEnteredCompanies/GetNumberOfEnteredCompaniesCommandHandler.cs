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
    public class GetNumberOfEnteredCompaniesCommandHandler : IRequestHandler<GetNumberOfEnteredCompaniesCommand, GetNumberOfEnteredCompaniesCommandResult>
    {
        private readonly ILogger<GetNumberOfEnteredCompaniesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNumberOfEnteredCompaniesCommandHandler(ILogger<GetNumberOfEnteredCompaniesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNumberOfEnteredCompaniesCommandResult> Handle(GetNumberOfEnteredCompaniesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getNumberOfCompanies = await _expoAPIQueryRepository.GetNumberOfEnteredCompaniesAsync(cancellationToken);

                _logger.LogInformation("The number of companies information has been fetched.");

                return new GetNumberOfEnteredCompaniesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    NumberOfEnteredCompanies = getNumberOfCompanies,
                    ReturnPath = $"/companies/entered/count"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNumberOfEnteredCompaniesCommandResult()
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
                    ReturnPath = $"/companies/entered/count"
                };
            }
        }
    }
}
