using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpoAPI.Contracts;
using ExpoAPI.ErrorCodes;
using ExpoAPI.Infrastructure.Adapters;
using ExpoAPI.Infrastructure.Repositories;
using MediatR;
using System;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ExpoAPI.UseCases.Accommodation
{
    public class CreateAccommodationCommandHandler : IRequestHandler<CreateAccommodationCommand, CreateAccommodationCommandResult>
    {
        private readonly ILogger<CreateAccommodationCommandHandler> _logger;
        private readonly IExpoAPIQueryRepository _expoAPIQueryRepository;
        private readonly IHashingAdapter _hashingAdapter;

        public CreateAccommodationCommandHandler(ILogger<CreateAccommodationCommandHandler> logger, IExpoAPIQueryRepository expoAPIQueryRepository,IHashingAdapter hashingAdapter)
        {
            _expoAPIQueryRepository = expoAPIQueryRepository;
            _logger = logger;
            _hashingAdapter = hashingAdapter;
        }

        public async Task<CreateAccommodationCommandResult> Handle(CreateAccommodationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var createAccommodation = await _expoAPIQueryRepository.CreateAccommodationAsync(command.Contract, cancellationToken);

                _logger.LogInformation("The accommodation has been created.");

                return new CreateAccommodationCommandResult()
                {
                    ValidateState = ValidationState.Valid,
                    ReturnPath = $"/accommodations"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong : @{Message} ", ex.Message);
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append(@"INSERT INTO ACCOMMODATION (CompanyName,")
                                        .Append("Hotel").Append(",")
                                        .Append("CheckInDate").Append(",")
                                        .Append("CheckInTime").Append(",")
                                        .Append("FirstGuest").Append(",")
                                        .Append("SecondGuest").Append(",")
                                        .Append("ThirdGuest").Append(",")
                                        .Append("NumberOfGuests").Append(",")
                                        .Append("GuestCompanyName").Append(",")
                                        .Append("Phone").Append(",")
                                        .Append("SNG").Append(",")
                                        .Append("DBL").Append(",")
                                        .Append("TRPL").Append(",")
                                        .Append("QUAT").Append(",")
                                        .Append("SNGCHD").Append(",")
                                        .Append("DBLCHD").Append(",")
                                        .Append("TRPLCHD").Append(",")
                                        .Append("CheckOutDate").Append(",")
                                        .Append("CheckOutTime").Append(",")
                                        .Append("_SNG").Append(",")
                                        .Append("_DBL").Append(",")
                                        .Append("_TRPL").Append(",")
                                        .Append("_QUAT").Append(",")
                                        .Append("_SNGCHD").Append(",")
                                        .Append("_DBLCHD").Append(",")
                                        .Append("_TRPLCHD").Append(",")
                                        .Append("Description").Append(") VALUES (")
                                        .Append(command.Contract.CompanyName == null ? "\'\'" : "\'"+command.Contract.CompanyName+"\'").Append(",")
                                        .Append(command.Contract.Hotel == null ? "\'\'" : "\'"+command.Contract.Hotel+"\'").Append(",")
                                        .Append("CAST(\'").Append(command.Contract.CheckIn.Value.ToShortDateString() == null ? "\'\'" : command.Contract.CheckIn.Value.ToUniversalTime().ToString()).Append("\' AS DATE)").Append(",")
                                        .Append("CAST(\'").Append(command.Contract.CheckIn.Value.ToShortTimeString() == null ? "\'\'" : command.Contract.CheckIn).Append("\' AS TIME)").Append(",")
                                        .Append(command.Contract.FirstGuest == null ? "\'\'" : "\'"+command.Contract.FirstGuest+"\'").Append(",")
                                        .Append(command.Contract.SecondGuest == null ? "\'\'" : "\'"+command.Contract.SecondGuest+"\'").Append(",")
                                        .Append(command.Contract.ThirdGuest == null ? "\'\'" : "\'"+command.Contract.ThirdGuest+"\'").Append(",")
                                        .Append(command.Contract.NumberOfGuests == null ? "\'\'" : command.Contract.NumberOfGuests).Append(",")
                                        .Append(command.Contract.GuestCompanyName == null ? "\'\'" : "\'"+command.Contract.GuestCompanyName+"\'").Append(",")
                                        .Append(command.Contract.Phone == null ? "\'\'" : "\'"+command.Contract.Phone+"\'").Append(",")
                                        .Append(command.Contract.SNG == null ? "\'\'" : "\'"+command.Contract.SNG+"\'").Append(",")
                                        .Append(command.Contract.DBL == null ? "\'\'" : "\'"+command.Contract.DBL+"\'").Append(",")
                                        .Append(command.Contract.TRPL == null ? "\'\'" : "\'"+command.Contract.TRPL+"\'").Append(",")
                                        .Append(command.Contract.QUAT == null ? "\'\'" : "\'"+command.Contract.QUAT+"\'").Append(",")
                                        .Append(command.Contract.SNGCHD == null ? "\'\'" : "\'"+command.Contract.SNGCHD+"\'").Append(",")
                                        .Append(command.Contract.DBLCHD == null ? "\'\'" : "\'"+command.Contract.DBLCHD+"\'").Append(",")
                                        .Append(command.Contract.TRPLCHD == null ? "\'\'" : "\'"+command.Contract.TRPLCHD+"\'").Append(",")
                                        .Append("CAST(\'").Append(command.Contract.CheckOut.Value.ToShortDateString() == null ? "\'\'" : command.Contract.CheckOut.Value.ToUniversalTime().ToString()).Append("\' AS DATE)").Append(",")
                                        .Append("CAST(\'").Append(command.Contract.CheckOut.Value.ToShortTimeString() == null ? "\'\'" : command.Contract.CheckOut).Append("\' AS TIME)").Append(",")
                                        .Append(command.Contract._SNG == null ? "\'\'" : "\'"+command.Contract._SNG+"\'").Append(",")
                                        .Append(command.Contract._DBL == null ? "\'\'" : "\'"+command.Contract._DBL+"\'").Append(",")
                                        .Append(command.Contract._TRPL == null ? "\'\'" : "\'"+command.Contract._TRPL+"\'").Append(",")
                                        .Append(command.Contract._QUAT == null ? "\'\'" : "\'"+command.Contract._QUAT+"\'").Append(",")
                                        .Append(command.Contract._SNGCHD == null ? "\'\'" : "\'"+command.Contract._SNGCHD+"\'").Append(",")
                                        .Append(command.Contract._DBLCHD == null ? "\'\'" : "\'"+command.Contract._DBLCHD+"\'").Append(",")
                                        .Append(command.Contract._TRPLCHD == null ? "\'\'" : "\'"+command.Contract._TRPLCHD+"\'").Append(",")
                                        .Append(command.Contract.Description == null ? "\'\'" : "\'"+command.Contract.Description+"\'").Append(")");

                return new CreateAccommodationCommandResult()
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
                    Query = queryBuilder.ToString(),
                    ReturnPath = $"/accommodations"
                };
            }
        }
    }
}
