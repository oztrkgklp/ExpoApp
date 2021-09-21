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
    public class UpdatePurchaseByIdCommandHandler : IRequestHandler<UpdatePurchaseByIdCommand, UpdatePurchaseByIdCommandResult>
    {
        private readonly ILogger<UpdatePurchaseByIdCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdatePurchaseByIdCommandHandler(ILogger<UpdatePurchaseByIdCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdatePurchaseByIdCommandResult> Handle(UpdatePurchaseByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updatePurchaseById = await _expoAPIQueryRepository.UpdatePurchaseByIdAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The purchase with ID {@purchaseID} has been updated.", command.Contract.PurchaseID);
                
                return new UpdatePurchaseByIdCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/purchases/{command.Contract.PurchaseID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message, command.Contract.PurchaseID);

                return new UpdatePurchaseByIdCommandResult()
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
                    ReturnPath = $"/purchases/{command.Contract.PurchaseID}"
                };
            }
        }
    }
}
