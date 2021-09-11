using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace ExpoAPI.UseCases
{
    public class GetAdminInformationCommandHandler : IRequestHandler<GetAdminInformationCommand, GetAdminInformationCommandResult>
    {
        private readonly ILogger<GetAdminInformationCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public GetAdminInformationCommandHandler(ILogger<GetAdminInformationCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<GetAdminInformationCommandResult> Handle(GetAdminInformationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getAdminInformation = await _expoAPIQueryRepository.GetAdminInformationAsync(cancellationToken);

                if (getAdminInformation == null)
                {
                    _logger.LogError("Could not fetch the admin login information");

                    return new GetAdminInformationCommandResult()
                    {
                        ValidateState = ValidationState.DoesNotExist,
                        Messages = new List<MessageContract>()
                        {
                            new MessageContract()
                            {
                                Title = "Customer device not found",
                                Content = DomainErrorCodes.EDExpoAPI10000,
                                Code = nameof(DomainErrorCodes.EDExpoAPI10000),
                                Type = nameof(MessageType.Error)
                            }
                        },
                        ReturnPath = $"/admin"
                    };
                }

                _logger.LogInformation("The admin information has been fetched.");

                // string userName = getAdminInformation.UserName;               
                // var hashedUsername = _hashingAdapter.HashUser(userName);

                return new GetAdminInformationCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    AdminInformationContract = new AdminInformationContract()
                    {
                        UserName = getAdminInformation.UserName,
                        Password = getAdminInformation.Password
                    },
                    ReturnPath = $"/admin"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message}", ex.Message);

                return new GetAdminInformationCommandResult()
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
                    ReturnPath = $"/admin"
                };
            }
        }
    }
}
