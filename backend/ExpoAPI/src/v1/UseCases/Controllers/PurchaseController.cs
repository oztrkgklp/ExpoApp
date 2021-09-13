using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Purchase;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ExpoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("purchases/{purchaseId}")]
        [ProducesResponseType(typeof(GetPurchaseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPurchaseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetPurchaseByIdApiResponseContract>> GetPurchaseById( [FromHeader] GetPurchaseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getPurchaseById = await _mediator.Send(new GetPurchaseByIdCommand(contract.PurchaseID), cancellationToken);
            if (!getPurchaseById.Success)
            {
                return BadRequest(new GetPurchaseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getPurchaseById.ReturnPath,
                    Messages = getPurchaseById.Messages?.ToList(),
                });
            }
            
            return Ok(new GetPurchaseByIdApiResponseContract
            {
                Result = new PurchaseContract()
                {
                    PurchaseID = getPurchaseById.PurchaseContract.PurchaseID,
                    Seller = getPurchaseById.PurchaseContract.Seller,
                    Purchaser = getPurchaseById.PurchaseContract.Purchaser,
                    PurchaseDate = getPurchaseById.PurchaseContract.PurchaseDate,
                    PurchaseInfo = getPurchaseById.PurchaseContract.PurchaseInfo
                },
                ReturnPath = getPurchaseById.ReturnPath
            });
        }
    }
}