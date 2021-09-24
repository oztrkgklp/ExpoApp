using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetNumberOfPurchasesCommandHandler : IRequestHandler<GetNumberOfPurchasesCommand, GetNumberOfPurchasesCommandResult>
    {
        private readonly ILogger<GetNumberOfPurchasesCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetNumberOfPurchasesCommandHandler(ILogger<GetNumberOfPurchasesCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetNumberOfPurchasesCommandResult> Handle(GetNumberOfPurchasesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getNumberOfPurchases = await _expoAPIQueryRepository.GetNumberOfPurchasesAsync(cancellationToken);

                _logger.LogInformation("The number of purchases information has been fetched.");

                return new GetNumberOfPurchasesCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    NumberOfPurchases = getNumberOfPurchases,
                    ReturnPath = $"/purchases/count"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetNumberOfPurchasesCommandResult()
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
                    ReturnPath = $"/purchases/count"
                };
            }
        }
    }
}
