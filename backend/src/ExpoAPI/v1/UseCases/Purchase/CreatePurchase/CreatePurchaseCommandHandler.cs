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
    public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, CreatePurchaseCommandResult>
    {
        private readonly ILogger<CreatePurchaseCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreatePurchaseCommandHandler(ILogger<CreatePurchaseCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreatePurchaseCommandResult> Handle(CreatePurchaseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createPurchase = await _expoAPIQueryRepository.CreatePurchaseAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The purchase has been created.");

                return new CreatePurchaseCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/purchases"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message);

                return new CreatePurchaseCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = new List<MessageContract>()
                    {
                        new MessageContract()
                        {
                            Title = "Invalid Request",
                            Content = DomainErrorCodes.EDExpoAPI10002,
                            Code = nameof(DomainErrorCodes.EDExpoAPI10002),
                            Type = nameof(MessageType.Error)
                        }
                    },
                    ReturnPath = $"/purchases"
                };
            }
        }
    }
}
