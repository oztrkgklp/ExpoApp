using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompaniesCommandHandler : IRequestHandler<GetCompaniesCommand, GetCompaniesCommandResult>
    {
        private readonly ILogger<GetCompaniesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCompaniesCommandHandler(ILogger<GetCompaniesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCompaniesCommandResult> Handle(GetCompaniesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanies = await _expoAPIQueryRepository.GetCompaniesAsync(cancellationToken);

                _logger.LogInformation("The company has been deleted.");

                return new GetCompaniesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContracts = getCompanies.ToList(),
                    ReturnPath = $"/companies"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetCompaniesCommandResult()
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
