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
    public class GetPurchasesCommandHandler : IRequestHandler<GetPurchasesCommand, GetPurchasesCommandResult>
    {
        private readonly ILogger<GetPurchasesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetPurchasesCommandHandler(ILogger<GetPurchasesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetPurchasesCommandResult> Handle(GetPurchasesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getPurchases = await _expoAPIQueryRepository.GetPurchasesAsync(cancellationToken);

                if (getPurchases == null)
                {
                    _logger.LogError("Could not fetch the purchases.");

                    return new GetPurchasesCommandResult()
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
                        ReturnPath = $"/purchases"
                    };
                }

                _logger.LogInformation("The purchases have been fetched.");

                List<PurchaseContract?>? list = new List<PurchaseContract>();
                foreach(PurchaseContract? item in getPurchases)
                {
                    var dbObject = new PurchaseContract()
                    {
                        PurchaseID = item.PurchaseID,
                        SellerID = item.SellerID,
                        PurchaserID = item.PurchaserID,
                        PurchaseDate = item.PurchaseDate,
                        Amount = item.Amount
                    };
                    list.Add(dbObject);
                }

                return new GetPurchasesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    PurchaseContracts = list,
                    ReturnPath = $"/purchases"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message}", ex.Message);

                return new GetPurchasesCommandResult()
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
                    ReturnPath = $"/purchases"
                };
            }
        }
    }
}
