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
    public class GetPurchasesWithCompanyNameCommandHandler : IRequestHandler<GetPurchasesWithCompanyNameCommand, GetPurchasesWithCompanyNameCommandResult>
    {
        private readonly ILogger<GetPurchasesWithCompanyNameCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetPurchasesWithCompanyNameCommandHandler(ILogger<GetPurchasesWithCompanyNameCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetPurchasesWithCompanyNameCommandResult> Handle(GetPurchasesWithCompanyNameCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getPurchases = await _expoAPIQueryRepository.GetPurchasesWithCompanyNamesAsync(cancellationToken);

                if (getPurchases == null)
                {
                    _logger.LogError("Could not fetch the purchases.");

                    return new GetPurchasesWithCompanyNameCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Purchases are not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/purchases/with-name"
                    };
                }

                _logger.LogInformation("The purchases have been fetched.");

                List<PurchaseWithNamesContract?>? list = new List<PurchaseWithNamesContract>();
                foreach(PurchaseWithNamesContract? item in getPurchases)
                {
                    var dbObject = new PurchaseWithNamesContract()
                    {
                        PurchaseID = item.PurchaseID,
                        SellerName = item.SellerName,
                        PurchaserName = item.PurchaserName,
                        Product = item.Product,
                        PurchaseDate = item.PurchaseDate,
                        Amount = item.Amount
                    };
                    list.Add(dbObject);
                }

                return new GetPurchasesWithCompanyNameCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    PurchaseContracts = list,
                    ReturnPath = $"/purchases/with-name"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message}", ex.Message);

                return new GetPurchasesWithCompanyNameCommandResult()
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
                    ReturnPath = $"/purchases/with-name"
                };
            }
        }
    }
}
