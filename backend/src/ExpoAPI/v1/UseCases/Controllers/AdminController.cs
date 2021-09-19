using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Admin;

namespace ExpoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("admin")]
        [ProducesResponseType(typeof(GetAdminInformationApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetAdminInformationApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetAdminInformationApiResponseContract>> GetAdminInformation( [FromForm] GetAdminInformationApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAdminInformation = await _mediator.Send(new GetAdminInformationCommand(), cancellationToken);

            if (!getAdminInformation.Success)
            {
                return BadRequest(new GetAdminInformationApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAdminInformation.ReturnPath,
                    Messages = getAdminInformation.Messages?.ToList(),
                });
            }

            return Ok(new GetAdminInformationApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAdminInformation.Messages?.ToList(),
                Result = getAdminInformation.AdminInformationContract,
                ReturnPath = getAdminInformation.ReturnPath
            });
        }
    }
}