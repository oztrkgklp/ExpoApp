using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExpoAPI.UseCases.Purchase
{
    public class DeletePurchaseByIdCommandHandler : IRequestHandler<DeletePurchaseByIdCommand, DeletePurchaseByIdCommandResult>
    {
        private readonly ILogger<DeletePurchaseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public DeletePurchaseByIdCommandHandler(ILogger<DeletePurchaseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<DeletePurchaseByIdCommandResult> Handle(DeletePurchaseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deletePurchaseById = await _expoAPIQueryRepository.DeletePurchaseByIdAsync(command.PurchaseID, cancellationToken);

                _logger.LogInformation("The purchase with ID {@purchaseID} has been deleted.", command.PurchaseID);
                
                return new DeletePurchaseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/purchases/{command.PurchaseID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message, command.PurchaseID);

                return new DeletePurchaseByIdCommandResult()
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
                    ReturnPath = $"/purchases/{command.PurchaseID}"
                };
            }
        }
    }
}
