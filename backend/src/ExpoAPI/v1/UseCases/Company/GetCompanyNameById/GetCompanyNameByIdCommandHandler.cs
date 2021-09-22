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
    public class GetCompanyNameByIdCommandHandler : IRequestHandler<GetCompanyNameByIdCommand, GetCompanyNameByIdCommandResult>
    {
        private readonly ILogger<GetCompanyNameByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCompanyNameByIdCommandHandler(ILogger<GetCompanyNameByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCompanyNameByIdCommandResult> Handle(GetCompanyNameByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanyNameById = await _expoAPIQueryRepository.GetCompanyNameByIdAsync(command.CompanyID, cancellationToken);

                _logger.LogInformation("The name of company with id {@name} has been fetched.",command.CompanyID);

                return new GetCompanyNameByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyName = getCompanyNameById,
                    ReturnPath = $"/companies/id?companyId={command.CompanyID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetCompanyNameByIdCommandResult()
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
                    ReturnPath = $"/companies/id?companyId={command.CompanyID}"
                };
            }
        }
    }
}
