using System;
using System.Linq;
using ExpoAPI.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ExpoAPI.UseCases;

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
        public async Task<ActionResult<GetAdminInformationApiResponseContract>> GetAdminInformation(CancellationToken cancellationToken)
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
                Result = getAdminInformation.AdminInformationContract,
                ReturnPath = getAdminInformation.ReturnPath
            });
        }
    }
}