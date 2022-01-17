using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.OtelInformation;
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
    public class OtelInformationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OtelInformationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("otel-info")]
        [ProducesResponseType(typeof(GetOtelInformationApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetOtelInformationApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetOtelInformationApiResponseContract>> GetOtelInformation( [FromForm] GetOtelInformationApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getOtelInformation = await _mediator.Send(new GetOtelInformationCommand(), cancellationToken);

            if (!getOtelInformation.Success)
            {
                return BadRequest(new GetOtelInformationApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getOtelInformation.ReturnPath,
                    Messages = getOtelInformation.Messages?.ToList(),
                });
            }

            return Ok(new GetOtelInformationApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getOtelInformation.Messages?.ToList(),
                Result = getOtelInformation.OtelInformationContract,
                ReturnPath = getOtelInformation.ReturnPath
            });
        }

        [HttpPut("otel-info")]
        [ProducesResponseType(typeof(UpdateOtelInformationApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateOtelInformationApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateOtelInformationApiResponseContract>> UpdateOtelInformation( [FromQuery] UpdateOtelInformationApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateOtelInformation = await _mediator.Send(new UpdateOtelInformationCommand(new OtelInformationContract()
                                                                                            {
                                                                                                OtelInformationID = contract.OtelInformationID,
                                                                                                SNG = contract.SNG,
                                                                                                DBL = contract.DBL,
                                                                                                TRPL = contract.TRPL,
                                                                                                QUAT = contract.QUAT,
                                                                                                SNGCHD = contract.SNGCHD,
                                                                                                DBLCHD = contract.DBLCHD,
                                                                                                TRPLCHD = contract.TRPLCHD
                                                                                            }), cancellationToken);

            if (!updateOtelInformation.Success)
            {
                return BadRequest(new UpdateOtelInformationApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateOtelInformation.ReturnPath,
                    Messages = updateOtelInformation.Messages?.ToList(),
                });
            }

            return Ok(new UpdateOtelInformationApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateOtelInformation.Messages?.ToList(),
                ReturnPath = updateOtelInformation.ReturnPath
            });
        }
    }
}