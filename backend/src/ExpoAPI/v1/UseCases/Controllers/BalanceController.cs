using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Balance;
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
    public class BalanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("balance")]
        [ProducesResponseType(typeof(GetBalanceApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetBalanceApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetBalanceApiResponseContract>> GetBalance( [FromForm] GetBalanceApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getBalance = await _mediator.Send(new GetBalanceCommand(), cancellationToken);

            if (!getBalance.Success)
            {
                return BadRequest(new GetBalanceApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getBalance.ReturnPath,
                    Messages = getBalance.Messages?.ToList(),
                });
            }

            return Ok(new GetBalanceApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getBalance.Messages?.ToList(),
                Result = getBalance.BalanceContract,
                ReturnPath = getBalance.ReturnPath
            });
        }

        [HttpPut("balance")]
        [ProducesResponseType(typeof(UpdateBalanceApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateBalanceApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateBalanceApiResponseContract>> UpdateBalance( [FromQuery] UpdateBalanceApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateBalance = await _mediator.Send(new UpdateBalanceCommand(new BalanceContract()
                                                                                            {
                                                                                                BalanceID = contract.BalanceID,
                                                                                                Amount = contract.Amount,
                                                                                            }), cancellationToken);

            if (!updateBalance.Success)
            {
                return BadRequest(new UpdateBalanceApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateBalance.ReturnPath,
                    Messages = updateBalance.Messages?.ToList(),
                });
            }

            return Ok(new UpdateBalanceApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateBalance.Messages?.ToList(),
                ReturnPath = updateBalance.ReturnPath
            });
        }
    }
}