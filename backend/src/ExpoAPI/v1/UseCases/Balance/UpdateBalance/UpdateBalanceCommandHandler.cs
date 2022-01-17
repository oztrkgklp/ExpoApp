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

namespace ExpoAPI.UseCases.Balance
{
    public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, UpdateBalanceCommandResult>
    {
        private readonly ILogger<UpdateBalanceCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public UpdateBalanceCommandHandler(ILogger<UpdateBalanceCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<UpdateBalanceCommandResult> Handle(UpdateBalanceCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var updateBalance = await _expoAPIQueryRepository.UpdateBalanceAsync(command.BalanceContract,cancellationToken);

                _logger.LogInformation("The balance has been updated.");

                return new UpdateBalanceCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/balance/{command.BalanceContract.BalanceID}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} -> @{Id}", ex.Message,command.BalanceContract.BalanceID);

                return new UpdateBalanceCommandResult()
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
                    ReturnPath = $"/balance/{command.BalanceContract.BalanceID}"
                };
            }
        }
    }
}
