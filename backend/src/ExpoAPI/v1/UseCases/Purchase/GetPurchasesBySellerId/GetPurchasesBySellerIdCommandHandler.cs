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
    public class GetPurchasesBySellerIdCommandHandler : IRequestHandler<GetPurchasesBySellerIdCommand, GetPurchasesBySellerIdCommandResult>
    {
        private readonly ILogger<GetPurchasesBySellerIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetPurchasesBySellerIdCommandHandler(ILogger<GetPurchasesBySellerIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetPurchasesBySellerIdCommandResult> Handle(GetPurchasesBySellerIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getPurchasesBySellerId = await _expoAPIQueryRepository.GetPurchasesBySellerIdAsync(command.SellerID, cancellationToken);
                if (getPurchasesBySellerId == null)
                {
                    _logger.LogError("Could not fetch the purchase with ID {@sellerID}.", command.SellerID);

                    return new GetPurchasesBySellerIdCommandResult()
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
                        ReturnPath = $"companies/{command.SellerID}/sales"
                    };
                }

                _logger.LogInformation("The purchase with ID {@purchaseID} has been fetched.", command.SellerID);
                
                List<PurchaseContract?>? list = new List<PurchaseContract>();
                foreach(PurchaseContract? item in getPurchasesBySellerId)
                {
                    var dbObject = new PurchaseContract()
                    {
                        PurchaseID = item.PurchaseID,
                        SellerID = item.SellerID,
                        PurchaserID = item.PurchaserID,
                        Product= item.Product,
                        PurchaseDate = item.PurchaseDate,
                        Amount = item.Amount
                    };
                    list.Add(dbObject);
                }

                return new GetPurchasesBySellerIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    PurchaseContracts = list,
                    ReturnPath = $"companies/{command.SellerID}/sales"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message, command.SellerID);

                return new GetPurchasesBySellerIdCommandResult()
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
                    ReturnPath = $"companies/{command.SellerID}/sales"
                };
            }
        }
    }
}
