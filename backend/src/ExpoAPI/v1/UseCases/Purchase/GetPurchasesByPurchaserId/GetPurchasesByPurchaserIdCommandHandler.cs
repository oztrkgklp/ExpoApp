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

namespace ExpoAPI.UseCases.Purchase
{
    public class GetPurchasesByPurchaserIdCommandHandler : IRequestHandler<GetPurchasesByPurchaserIdCommand, GetPurchasesByPurchaserIdCommandResult>
    {
        private readonly ILogger<GetPurchasesByPurchaserIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetPurchasesByPurchaserIdCommandHandler(ILogger<GetPurchasesByPurchaserIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetPurchasesByPurchaserIdCommandResult> Handle(GetPurchasesByPurchaserIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getPurchasesByPurchaserId = await _expoAPIQueryRepository.GetPurchasesByPurchaserIdAsync(command.PurchaserID, cancellationToken);
                if (getPurchasesByPurchaserId == null)
                {
                    _logger.LogError("Could not fetch the purchase with ID {@PurchaserID}.", command.PurchaserID);

                    return new GetPurchasesByPurchaserIdCommandResult()
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
                        ReturnPath = $"companies/{command.PurchaserID}/purchases"
                    };
                }

                _logger.LogInformation("The purchase with ID {@purchaseID} has been fetched.", command.PurchaserID);
                
                List<PurchaseContract?>? list = new List<PurchaseContract>();
                foreach(PurchaseContract? item in getPurchasesByPurchaserId)
                {
                    var dbObject = new PurchaseContract()
                    {
                        PurchaseID = item.PurchaseID,
                        PurchaserID = item.PurchaserID,
                        SellerID = item.SellerID,
                        Product = item.Product,
                        PurchaseDate = item.PurchaseDate,
                        Amount = item.Amount
                    };
                    list.Add(dbObject);
                }

                return new GetPurchasesByPurchaserIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    PurchaseContracts = list,
                    ReturnPath = $"companies/{command.PurchaserID}/purchases"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message, command.PurchaserID);

                return new GetPurchasesByPurchaserIdCommandResult()
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
                    ReturnPath = $"companies/{command.PurchaserID}/purchases"
                };
            }
        }
    }
}
