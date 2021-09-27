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
    public class GetNumberOfCompaniesCommandHandler : IRequestHandler<GetNumberOfCompaniesCommand, GetNumberOfCompaniesCommandResult>
    {
        private readonly ILogger<GetNumberOfCompaniesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNumberOfCompaniesCommandHandler(ILogger<GetNumberOfCompaniesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNumberOfCompaniesCommandResult> Handle(GetNumberOfCompaniesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getNumberOfCompanies = await _expoAPIQueryRepository.GetNumberOfCompaniesAsync(cancellationToken);

                _logger.LogInformation("The number of companies information has been fetched.");

                return new GetNumberOfCompaniesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    NumberOfCompanies = getNumberOfCompanies,
                    ReturnPath = $"/companies/count"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNumberOfCompaniesCommandResult()
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
                    ReturnPath = $"/companies/count"
                };
            }
        }
    }
}
