using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Purchase;

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

        [HttpGet("purchases")]
        [ProducesResponseType(typeof(GetPurchasesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPurchasesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetPurchasesApiResponseContract>> GetPurchases( [FromForm] GetPurchasesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getPurchases = await _mediator.Send(new GetPurchasesCommand(), cancellationToken);
            if (!getPurchases.Success)
            {
                return BadRequest(new GetPurchasesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getPurchases.ReturnPath,
                    Messages = getPurchases.Messages?.ToList(),
                });
            }
            
            return Ok(new GetPurchasesApiResponseContract
            {
                Result = getPurchases.PurchaseContracts,
                ReturnPath = getPurchases.ReturnPath
            });
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
                    SellerID = getPurchaseById.PurchaseContract.SellerID,
                    PurchaserID = getPurchaseById.PurchaseContract.PurchaserID,
                    PurchaseDate = getPurchaseById.PurchaseContract.PurchaseDate,
                    Amount = getPurchaseById.PurchaseContract.Amount
                },
                ReturnPath = getPurchaseById.ReturnPath
            });
        }

        [HttpGet("companies/{sellerId}/sales")]
        [ProducesResponseType(typeof(GetPurchasesBySellerIdResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPurchasesBySellerIdResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetPurchasesBySellerIdResponseContract>> GetPurchasesBySellerId( [FromHeader] GetPurchasesBySellerIdRequestContract contract, CancellationToken cancellationToken)
        {
            var getPurchaseBySellerId = await _mediator.Send(new GetPurchasesBySellerIdCommand(contract.SellerID), cancellationToken);
            if (!getPurchaseBySellerId.Success)
            {
                return BadRequest(new GetPurchasesBySellerIdResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getPurchaseBySellerId.ReturnPath,
                    Messages = getPurchaseBySellerId.Messages?.ToList(),
                });
            }
            
            return Ok(new GetPurchasesBySellerIdResponseContract
            {
                Result = getPurchaseBySellerId.PurchaseContracts,
                ReturnPath = getPurchaseBySellerId.ReturnPath
            });
        }

        [HttpGet("companies/{purchaserId}/purchases")]
        [ProducesResponseType(typeof(GetPurchasesByPurchaserIdResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetPurchasesByPurchaserIdResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetPurchasesByPurchaserIdResponseContract>> GetPurchasesByPurchaserId( [FromHeader] GetPurchasesByPurchaserIdRequestContract contract, CancellationToken cancellationToken)
        {
            var getPurchaseBySellerId = await _mediator.Send(new GetPurchasesBySellerIdCommand(contract.PurchaserID), cancellationToken);
            if (!getPurchaseBySellerId.Success)
            {
                return BadRequest(new GetPurchasesByPurchaserIdResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getPurchaseBySellerId.ReturnPath,
                    Messages = getPurchaseBySellerId.Messages?.ToList(),
                });
            }
            
            return Ok(new GetPurchasesByPurchaserIdResponseContract
            {
                Result = getPurchaseBySellerId.PurchaseContracts,
                ReturnPath = getPurchaseBySellerId.ReturnPath
            });
        }


        [HttpPost("purchases")]
        [ProducesResponseType(typeof(CreatePurchaseApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreatePurchaseApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreatePurchaseApiResponseContract>> CreatePurchase( [FromQuery] CreatePurchaseApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createPurchase = await _mediator.Send(new CreatePurchaseCommand(new PurchaseContract()
                                                                                    {
                                                                                        SellerID = contract.SellerID,
                                                                                        PurchaserID = contract.PurchaserID,
                                                                                        PurchaseDate = DateTime.Now,
                                                                                        Amount = contract.Amount
                                                                                    }), cancellationToken);
            if (!createPurchase.Success)
            {
                return BadRequest(new CreatePurchaseApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createPurchase.ReturnPath,
                    Messages = createPurchase.Messages?.ToList(),
                });
            }
            
            return Ok(new CreatePurchaseApiResponseContract
            {
                ReturnPath = createPurchase.ReturnPath
            });
        }

        [HttpPut("purchases")]
        [ProducesResponseType(typeof(UpdatePurchaseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdatePurchaseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdatePurchaseByIdApiResponseContract>> UpdatePurchase( [FromQuery] UpdatePurchaseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createPurchase = await _mediator.Send(new UpdatePurchaseByIdCommand(new PurchaseContract()
                                                                                    {
                                                                                        PurchaseID = contract.PurchaseID,
                                                                                        SellerID = contract.SellerID,
                                                                                        PurchaserID = contract.PurchaserID,
                                                                                        PurchaseDate = DateTime.Now,
                                                                                        Amount = contract.Amount
                                                                                    }), cancellationToken);
            if (!createPurchase.Success)
            {
                return BadRequest(new UpdatePurchaseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createPurchase.ReturnPath,
                    Messages = createPurchase.Messages?.ToList(),
                });
            }
            
            return Ok(new UpdatePurchaseByIdApiResponseContract
            {
                ReturnPath = createPurchase.ReturnPath
            });
        }

        [HttpDelete("purchases/{purchaseId}")]
        [ProducesResponseType(typeof(DeletePurchaseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeletePurchaseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeletePurchaseByIdApiResponseContract>> DeletePurchase( [FromHeader] DeletePurchaseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deletePurchaseById = await _mediator.Send(new DeletePurchaseByIdCommand(contract.PurchaseID), cancellationToken);
            if (!deletePurchaseById.Success)
            {
                return BadRequest(new DeletePurchaseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deletePurchaseById.ReturnPath,
                    Messages = deletePurchaseById.Messages?.ToList(),
                });
            }
            
            return Ok(new DeletePurchaseByIdApiResponseContract
            {
                ReturnPath = deletePurchaseById.ReturnPath
            });
        }
    }
}