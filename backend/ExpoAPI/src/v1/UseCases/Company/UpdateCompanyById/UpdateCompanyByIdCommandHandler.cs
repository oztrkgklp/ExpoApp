using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class UpdateCompanyByIdCommandHandler : IRequestHandler<UpdateCompanyByIdCommand, UpdateCompanyByIdCommandResult>
    {
        private readonly ILogger<UpdateCompanyByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateCompanyByIdCommandHandler(ILogger<UpdateCompanyByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateCompanyByIdCommandResult> Handle(UpdateCompanyByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getCompanyById = await _expoAPIQueryRepository.UpdateCompanyByIdAsync(command.CompanyContract,cancellationToken);

                _logger.LogInformation("The company with id {@companyId} has been updated.",command.CompanyContract.CompanyID);

                return new UpdateCompanyByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/companies/{command.CompanyContract.CompanyID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.CompanyContract.CompanyID);

                return new UpdateCompanyByIdCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10001,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10001),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/companies/{command.CompanyContract.CompanyID}"
                };
            }
        }
    }
}
