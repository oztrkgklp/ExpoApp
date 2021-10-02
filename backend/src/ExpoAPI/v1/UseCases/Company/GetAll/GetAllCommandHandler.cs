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

namespace ExpoAPI.UseCases.Company
{
    public class GetAllCommandHandler : IRequestHandler<GetAllCommand, GetAllCommandResult>
    {
        private readonly ILogger<GetAllCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetAllCommandHandler(ILogger<GetAllCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetAllCommandResult> Handle(GetAllCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getAll = await _expoAPIQueryRepository.GetAllAsync(cancellationToken);

                _logger.LogInformation("The company data has been fetched.");

                return new GetAllCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    CompanyContracts = getAll.ToList(),
                    ReturnPath = $"/all"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);

                return new GetAllCommandResult()
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
                    ReturnPath = $"/all"
                };
            }
        }
    }
}
