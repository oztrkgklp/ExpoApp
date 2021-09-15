using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;

namespace ExpoAPI.UseCases.Company
{
    public class DeleteCompanyByIdCommandHandler : IRequestHandler<DeleteCompanyByIdCommand, DeleteCompanyByIdCommandResult>
    {
        private readonly ILogger<DeleteCompanyByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeleteCompanyByIdCommandHandler(ILogger<DeleteCompanyByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeleteCompanyByIdCommandResult> Handle(DeleteCompanyByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleteCompanyById = await _expoAPIQueryRepository.DeleteCompanyByIdAsync(command.CompanyID, cancellationToken);

                _logger.LogInformation("The company has been deleted.");

                return new DeleteCompanyByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/companies/{command.CompanyID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new DeleteCompanyByIdCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10003,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10003),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/companies/{command.CompanyID}"
                };
            }
        }
    }
}
