using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.ExternalAttendance;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System;
using System.Linq;

namespace ExpoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class ExternalAttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExternalAttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("external-attendances")]
        [ProducesResponseType(typeof(GetExternalAttendancesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetExternalAttendancesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetExternalAttendancesApiResponseContract>> GetExternalAttendances( [FromForm] GetExternalAttendancesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAdminInformation = await _mediator.Send(new GetExternalAttendancesCommand(), cancellationToken);

            if (!getAdminInformation.Success)
            {
                return BadRequest(new GetExternalAttendancesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAdminInformation.ReturnPath,
                    Messages = getAdminInformation.Messages?.ToList(),
                });
            }

            return Ok(new GetExternalAttendancesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAdminInformation.Messages?.ToList(),
                Result = getAdminInformation.ExternalAttendanceContracts,
                ReturnPath = getAdminInformation.ReturnPath
            });
        }

        [HttpGet("external-attendances/{externalAttendanceId}")]
        [ProducesResponseType(typeof(GetExternalAttendanceByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetExternalAttendanceByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetExternalAttendanceByIdApiResponseContract>> GetExternalAttendanceById( [FromHeader] GetExternalAttendanceByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getExternalAttendanceById = await _mediator.Send(new GetExternalAttendanceByIdCommand(contract.ExternalAttendanceID), cancellationToken);

            if (!getExternalAttendanceById.Success)
            {
                return BadRequest(new GetExternalAttendanceByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getExternalAttendanceById.ReturnPath,
                    Messages = getExternalAttendanceById.Messages?.ToList(),
                });
            }

            return Ok(new GetExternalAttendanceByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getExternalAttendanceById.Messages?.ToList(),
                Result = getExternalAttendanceById.ExternalAttendanceContract,
                ReturnPath = getExternalAttendanceById.ReturnPath
            });
        }

        [HttpDelete("external-attendances/{externalAttendanceId}")]
        [ProducesResponseType(typeof(DeleteExternalAttendanceByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteExternalAttendanceByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteExternalAttendanceByIdApiResponseContract>> DeleteExternalAttendanceById( [FromHeader] DeleteExternalAttendanceByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deleteExternalAttendanceById = await _mediator.Send(new DeleteExternalAttendanceByIdCommand(contract.ExternalAttendanceID), cancellationToken);

            if (!deleteExternalAttendanceById.Success)
            {
                return BadRequest(new DeleteExternalAttendanceByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deleteExternalAttendanceById.ReturnPath,
                    Messages = deleteExternalAttendanceById.Messages?.ToList(),
                });
            }

            return Ok(new DeleteExternalAttendanceByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = deleteExternalAttendanceById.Messages?.ToList(),
                ReturnPath = deleteExternalAttendanceById.ReturnPath
            });
        }

        [HttpPost("external-attendances")]
        [ProducesResponseType(typeof(CreateExternalAttendanceApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateExternalAttendanceApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateExternalAttendanceApiResponseContract>> CreateExternalAttendance( [FromQuery] CreateExternalAttendanceApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createExternalAttendance = await _mediator.Send(new CreateExternalAttendanceCommand(new ExternalAttendanceContract()
                                                                {
                                                                    NameSurname = contract.NameSurname,
                                                                    TCID = contract.TCID,
                                                                    NumberOfPeople = contract.NumberOfPeople,
                                                                    CompanyName = contract.CompanyName,
                                                                    Phone = contract.Phone,
                                                                    EntranceTime = contract.EntranceTime,
                                                                    ExitTime = contract.ExitTime,
                                                                    Occupancy = contract.Occupancy,
                                                                    EntranceDate = contract.EntranceDate,
                                                                    Description = contract.Description
                                                                }), cancellationToken);

            if (!createExternalAttendance.Success)
            {
                return BadRequest(new CreateExternalAttendanceApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createExternalAttendance.ReturnPath,
                    Messages = createExternalAttendance.Messages?.ToList(),
                });
            }

            return Ok(new CreateExternalAttendanceApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = createExternalAttendance.Messages?.ToList(),
                ReturnPath = createExternalAttendance.ReturnPath
            });
        }

        [HttpPut("external-attendances/{externalAttendanceId}")]
        [ProducesResponseType(typeof(UpdateExternalAttendanceByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateExternalAttendanceByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateExternalAttendanceByIdApiResponseContract>> UpdateExternalAttendanceById([FromRoute]int ExternalAttendanceID, [FromQuery] UpdateExternalAttendanceByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateExternalAttendanceById = await _mediator.Send(new UpdateExternalAttendanceByIdCommand(new ExternalAttendanceContract()
                                                                {
                                                                    ExternalAttendanceID = ExternalAttendanceID,
                                                                    NameSurname = contract.NameSurname,
                                                                    TCID = contract.TCID,
                                                                    CompanyName = contract.CompanyName,
                                                                    NumberOfPeople = contract.NumberOfPeople,
                                                                    Phone = contract.Phone,
                                                                    EntranceTime = contract.EntranceTime,
                                                                    ExitTime = contract.ExitTime,
                                                                    Occupancy = contract.Occupancy,
                                                                    EntranceDate = contract.EntranceDate,
                                                                    Description = contract.Description,
                                                                }), cancellationToken);

            if (!updateExternalAttendanceById.Success)
            {
                return BadRequest(new UpdateExternalAttendanceByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateExternalAttendanceById.ReturnPath,
                    Messages = updateExternalAttendanceById.Messages?.ToList(),
                });
            }

            return Ok(new UpdateExternalAttendanceByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateExternalAttendanceById.Messages?.ToList(),
                ReturnPath = updateExternalAttendanceById.ReturnPath
            });
        }

    }
}