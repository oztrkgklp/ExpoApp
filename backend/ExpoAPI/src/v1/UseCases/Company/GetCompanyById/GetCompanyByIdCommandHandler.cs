using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class GetCompanyByIdCommandHandler : IRequestHandler<GetCompanyByIdCommand, GetCompanyByIdCommandResult>
    {
        private readonly ILogger<GetCompanyByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetCompanyByIdCommandHandler(ILogger<GetCompanyByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetCompanyByIdCommandResult> Handle(GetCompanyByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanyById = await _expoAPIQueryRepository.GetCompanyByIdAsync(command.CompanyID,cancellationToken);

                _logger.LogInformation("The company with id {@companyId} has been deleted.",command.CompanyID);

                return new GetCompanyByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContract = getCompanyById,
                    ReturnPath = $"/companies/{command.CompanyID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.CompanyID);

                return new GetCompanyByIdCommandResult()
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
                    ReturnPath = $"/companies/{command.CompanyID}"
                };
            }
        }
    }
}
