using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Cost;
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
    public class CostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("costs")]
        [ProducesResponseType(typeof(GetCostsApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCostsApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCostsApiResponseContract>> GetCosts( [FromForm] GetCostsApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAdminInformation = await _mediator.Send(new GetCostsCommand(), cancellationToken);

            if (!getAdminInformation.Success)
            {
                return BadRequest(new GetCostsApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAdminInformation.ReturnPath,
                    Messages = getAdminInformation.Messages?.ToList(),
                });
            }

            return Ok(new GetCostsApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAdminInformation.Messages?.ToList(),
                Result = getAdminInformation.CostContracts,
                ReturnPath = getAdminInformation.ReturnPath
            });
        }

        [HttpGet("costs/{CostId}")]
        [ProducesResponseType(typeof(GetCostByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCostByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCostByIdApiResponseContract>> GetCostById( [FromHeader] GetCostByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCostById = await _mediator.Send(new GetCostByIdCommand(contract.CostID), cancellationToken);

            if (!getCostById.Success)
            {
                return BadRequest(new GetCostByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCostById.ReturnPath,
                    Messages = getCostById.Messages?.ToList(),
                });
            }

            return Ok(new GetCostByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCostById.Messages?.ToList(),
                Result = getCostById.CostContract,
                ReturnPath = getCostById.ReturnPath
            });
        }

        [HttpDelete("costs/{CostId}")]
        [ProducesResponseType(typeof(DeleteCostByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteCostByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteCostByIdApiResponseContract>> DeleteCostById( [FromHeader] DeleteCostByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deleteCostById = await _mediator.Send(new DeleteCostByIdCommand(contract.CostID), cancellationToken);

            if (!deleteCostById.Success)
            {
                return BadRequest(new DeleteCostByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deleteCostById.ReturnPath,
                    Messages = deleteCostById.Messages?.ToList(),
                });
            }

            return Ok(new DeleteCostByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = deleteCostById.Messages?.ToList(),
                ReturnPath = deleteCostById.ReturnPath
            });
        }

        [HttpPost("costs")]
        [ProducesResponseType(typeof(CreateCostApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateCostApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCostApiResponseContract>> CreateCost( [FromQuery] CreateCostApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createCost = await _mediator.Send(new CreateCostCommand(new CostContract()
                                                                {
                                                                    CostType = contract.CostType,
                                                                    CostDate = contract.CostDate,
                                                                    Description = contract.Description,
                                                                    PAX = contract.PAX,
                                                                    TotalCost = contract.TotalCost
                                                                }), cancellationToken);

            if (!createCost.Success)
            {
                return BadRequest(new CreateCostApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createCost.ReturnPath,
                    Messages = createCost.Messages?.ToList(),
                });
            }

            return Ok(new CreateCostApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = createCost.Messages?.ToList(),
                ReturnPath = createCost.ReturnPath
            });
        }

        [HttpPut("costs/{CostId}")]
        [ProducesResponseType(typeof(UpdateCostByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateCostByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateCostByIdApiResponseContract>> UpdateCostById([FromRoute]int CostID, [FromQuery] UpdateCostByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateCostById = await _mediator.Send(new UpdateCostByIdCommand(new CostContract()
                                                                {
                                                                    CostID = CostID,
                                                                    CostType = contract.CostType,
                                                                    CostDate = contract.CostDate,
                                                                    Description = contract.Description,
                                                                    PAX = contract.PAX,
                                                                    TotalCost = contract.TotalCost
                                                                }), cancellationToken);

            if (!updateCostById.Success)
            {
                return BadRequest(new UpdateCostByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateCostById.ReturnPath,
                    Messages = updateCostById.Messages?.ToList(),
                });
            }

            return Ok(new UpdateCostByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateCostById.Messages?.ToList(),
                ReturnPath = updateCostById.ReturnPath
            });
        }

    }
}