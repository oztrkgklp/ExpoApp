using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchaseByIdCommandHandler : IRequestHandler<GetPurchaseByIdCommand, GetPurchaseByIdCommandResult>
    {
        private readonly ILogger<GetPurchaseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetPurchaseByIdCommandHandler(ILogger<GetPurchaseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetPurchaseByIdCommandResult> Handle(GetPurchaseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getPurchaseById = await _expoAPIQueryRepository.GetPurchaseByIdAsync(command.PurchaseID, cancellationToken);

                if (getPurchaseById == null)
                {
                    _logger.LogError("Could not fetch the purchase with ID {@purchaseID}.", command.PurchaseID);

                    return new GetPurchaseByIdCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Purchase not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/purchases/{command.PurchaseID}"
                    };
                }

                _logger.LogInformation("The purchase with ID {@purchaseID} has been fetched.", command.PurchaseID);
                
                return new GetPurchaseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    
                    PurchaseContract = new PurchaseContract()
                    {
                        PurchaseID = getPurchaseById.PurchaseID,
                        SellerID = getPurchaseById.SellerID,
                        PurchaserID = getPurchaseById.PurchaserID,
                        PurchaseDate = getPurchaseById.PurchaseDate,
                        Amount = getPurchaseById.Amount
                    },
                    ReturnPath = $"/purchases/{command.PurchaseID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message, command.PurchaseID);

                return new GetPurchaseByIdCommandResult()
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
                    ReturnPath = $"/purchases/{command.PurchaseID}"
                };
            }
        }
    }
}
