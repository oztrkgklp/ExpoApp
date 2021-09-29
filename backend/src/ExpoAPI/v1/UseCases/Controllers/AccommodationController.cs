using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Accommodation;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Globalization;
using System.Linq;

namespace ExpoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AccommodationController : ControllerBase
    {
        private readonly IMediator _mediator;
        CultureInfo culture;

        public AccommodationController(IMediator mediator)
        {
            _mediator = mediator;
            culture = new CultureInfo("en-US");
        }

        [HttpGet("accommodations")]
        [ProducesResponseType(typeof(GetAccommodationsApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAccommodationsApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAccommodationsApiResponseContract>> GetAccommodations( [FromQuery] GetAccommodationsApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAccommodations = await _mediator.Send(new GetAccommodationsCommand(), cancellationToken);

            if (!getAccommodations.Success)
            {
                return BadRequest(new GetAccommodationsApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAccommodations.ReturnPath,
                    Messages = getAccommodations.Messages?.ToList(),
                });
            }

            return Ok(new GetAccommodationsApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAccommodations.Messages?.ToList(),
                Result = getAccommodations.AccommodationContracts,
                ReturnPath = getAccommodations.ReturnPath
            });
        }

        [HttpGet("accommodations/count")]
        [ProducesResponseType(typeof(GetNumberOfAccommodationsApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfAccommodationsApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfAccommodationsApiResponseContract>> GetNumberOfAccommodations( [FromQuery] GetNumberOfAccommodationsApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getNumberOfAccommodations = await _mediator.Send(new GetNumberOfAccommodationsCommand(), cancellationToken);

            if (!getNumberOfAccommodations.Success)
            {
                return BadRequest(new GetNumberOfAccommodationsApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getNumberOfAccommodations.ReturnPath,
                    Messages = getNumberOfAccommodations.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfAccommodationsApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getNumberOfAccommodations.Messages?.ToList(),
                Result = getNumberOfAccommodations.NumberOfAccommodations,
                ReturnPath = getNumberOfAccommodations.ReturnPath
            });
        }

        [HttpGet("accommodations/{accommodationId}")]
        [ProducesResponseType(typeof(GetAccommodationByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAccommodationByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAccommodationByIdApiResponseContract>> GetAccommodationById( [FromHeader] GetAccommodationByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAccommodationById = await _mediator.Send(new GetAccommodationByIdCommand(contract.AccommodationID), cancellationToken);

            if (!getAccommodationById.Success)
            {
                return BadRequest(new GetAccommodationByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAccommodationById.ReturnPath,
                    Messages = getAccommodationById.Messages?.ToList(),
                });
            }

            return Ok(new GetAccommodationByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAccommodationById.Messages?.ToList(),
                Result = getAccommodationById.AccommodationContract,
                ReturnPath = getAccommodationById.ReturnPath
            });
        }

        [HttpDelete("accommodations/{accommodationId}")]
        [ProducesResponseType(typeof(DeleteAccommodationByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteAccommodationByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteAccommodationByIdApiResponseContract>> DeleteAccommodationById( [FromHeader] DeleteAccommodationByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deleteAccommodationById = await _mediator.Send(new DeleteAccommodationByIdCommand(contract.AccommodationID), cancellationToken);

            if (!deleteAccommodationById.Success)
            {
                return BadRequest(new DeleteAccommodationByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deleteAccommodationById.ReturnPath,
                    Messages = deleteAccommodationById.Messages?.ToList(),
                });
            }

            return Ok(new DeleteAccommodationByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = deleteAccommodationById.Messages?.ToList(),
                ReturnPath = deleteAccommodationById.ReturnPath
            });
        }

        [HttpPost("accommodations")]
        [ProducesResponseType(typeof(CreateAccommodationApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateAccommodationApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateAccommodationApiResponseContract>> CreateAccommodation( [FromQuery] CreateAccommodationApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createAccommodation = await _mediator.Send(new CreateAccommodationCommand(new AccommodationContract()
                                                                {
                                                                    CompanyName = contract.CompanyName,
                                                                    Hotel = contract.Hotel,
                                                                    CheckIn = Convert.ToDateTime(DateTime.Parse(contract.CheckIn.ToString()).ToString(culture)),
                                                                    FirstGuest = contract.FirstGuest,
                                                                    SecondGuest = contract.SecondGuest,
                                                                    ThirdGuest = contract.ThirdGuest,
                                                                    GuestCompanyName = contract.GuestCompanyName,
                                                                    Phone = contract.Phone,
                                                                    SNG = contract.SNG,
                                                                    DBL = contract.DBL,
                                                                    TRPL = contract.TRPL,
                                                                    QUAT = contract.QUAT,
                                                                    SNGCHD = contract.SNGCHD,
                                                                    DBLCHD = contract.DBLCHD,
                                                                    TRPLCHD = contract.TRPLCHD,
                                                                    CheckOut = Convert.ToDateTime(DateTime.Parse(contract.CheckOut.ToString()).ToString(culture)),
                                                                    _SNG = contract._SNG,
                                                                    _DBL = contract._DBL,
                                                                    _TRPL = contract._TRPL,
                                                                    _QUAT = contract._QUAT,
                                                                    _SNGCHD = contract._SNGCHD,
                                                                    _DBLCHD = contract._DBLCHD,
                                                                    _TRPLCHD = contract._TRPLCHD,
                                                                    Description = contract.Description
                                                                }), cancellationToken);

            if (!createAccommodation.Success)
            {
                return BadRequest(new CreateAccommodationApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createAccommodation.ReturnPath,
                    Messages = createAccommodation.Messages?.ToList(),
                });
            }

            return Ok(new CreateAccommodationApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = createAccommodation.Messages?.ToList(),
                ReturnPath = createAccommodation.ReturnPath
            });
        }

        [HttpPut("accommodations/{accommodationId}")]
        [ProducesResponseType(typeof(UpdateAccommodationByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateAccommodationByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateAccommodationByIdApiResponseContract>> UpdateAccommodationById([FromRoute]int accommodationId, [FromQuery] UpdateAccommodationByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateAccommodationById = await _mediator.Send(new UpdateAccommodationByIdCommand(new AccommodationContract()
                                                                {
                                                                    AccommodationID = accommodationId,
                                                                    CompanyName = contract.CompanyName,
                                                                    Hotel = contract.Hotel,
                                                                    CheckIn = Convert.ToDateTime(DateTime.Parse(contract.CheckIn.ToString()).ToString(culture)),
                                                                    FirstGuest = contract.FirstGuest,
                                                                    SecondGuest = contract.SecondGuest,
                                                                    ThirdGuest = contract.ThirdGuest,
                                                                    GuestCompanyName = contract.GuestCompanyName,
                                                                    Phone = contract.Phone,
                                                                    SNG = contract.SNG,
                                                                    DBL = contract.DBL,
                                                                    TRPL = contract.TRPL,
                                                                    QUAT = contract.QUAT,
                                                                    SNGCHD = contract.SNGCHD,
                                                                    DBLCHD = contract.DBLCHD,
                                                                    TRPLCHD = contract.TRPLCHD,
                                                                    CheckOut = Convert.ToDateTime(DateTime.Parse(contract.CheckOut.ToString()).ToString(culture)),
                                                                    _SNG = contract._SNG,
                                                                    _DBL = contract._DBL,
                                                                    _TRPL = contract._TRPL,
                                                                    _QUAT = contract._QUAT,
                                                                    _SNGCHD = contract._SNGCHD,
                                                                    _DBLCHD = contract._DBLCHD,
                                                                    _TRPLCHD = contract._TRPLCHD,
                                                                    Description = contract.Description
                                                                }), cancellationToken);

            if (!updateAccommodationById.Success)
            {
                return BadRequest(new UpdateAccommodationByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateAccommodationById.ReturnPath,
                    Messages = updateAccommodationById.Messages?.ToList(),
                });
            }

            return Ok(new UpdateAccommodationByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateAccommodationById.Messages?.ToList(),
                ReturnPath = updateAccommodationById.ReturnPath
            });
        }
    }
}