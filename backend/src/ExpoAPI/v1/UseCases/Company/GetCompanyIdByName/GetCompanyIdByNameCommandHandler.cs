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

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyIdByNameCommandHandler : IRequestHandler<GetCompanyIdByNameCommand, GetCompanyIdByNameCommandResult>
    {
        private readonly ILogger<GetCompanyIdByNameCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCompanyIdByNameCommandHandler(ILogger<GetCompanyIdByNameCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCompanyIdByNameCommandResult> Handle(GetCompanyIdByNameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanies = await _expoAPIQueryRepository.GetCompanyIdByNameAsync(command.CompanyName, cancellationToken);

                _logger.LogInformation("The id of company with name {@name} has been fetched.",command.CompanyName);

                return new GetCompanyIdByNameCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyID = getCompanies,
                    ReturnPath = $"/companies?name={command.CompanyName}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetCompanyIdByNameCommandResult()
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
                    ReturnPath = $"/companies?name={command.CompanyName}"
                };
            }
        }
    }
}
