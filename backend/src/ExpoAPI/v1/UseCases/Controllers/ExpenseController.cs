using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Expense;
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
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("expenses")]
        [ProducesResponseType(typeof(GetExpensesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetExpensesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetExpensesApiResponseContract>> GetExpenses( [FromForm] GetExpensesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getAdminInformation = await _mediator.Send(new GetExpensesCommand(), cancellationToken);

            if (!getAdminInformation.Success)
            {
                return BadRequest(new GetExpensesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getAdminInformation.ReturnPath,
                    Messages = getAdminInformation.Messages?.ToList(),
                });
            }

            return Ok(new GetExpensesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getAdminInformation.Messages?.ToList(),
                Result = getAdminInformation.ExpenseContracts,
                ReturnPath = getAdminInformation.ReturnPath
            });
        }

        [HttpGet("expenses/{ExpenseId}")]
        [ProducesResponseType(typeof(GetExpenseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetExpenseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetExpenseByIdApiResponseContract>> GetExpenseById( [FromHeader] GetExpenseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getExpenseById = await _mediator.Send(new GetExpenseByIdCommand(contract.ExpenseID), cancellationToken);

            if (!getExpenseById.Success)
            {
                return BadRequest(new GetExpenseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getExpenseById.ReturnPath,
                    Messages = getExpenseById.Messages?.ToList(),
                });
            }

            return Ok(new GetExpenseByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getExpenseById.Messages?.ToList(),
                Result = getExpenseById.ExpenseContract,
                ReturnPath = getExpenseById.ReturnPath
            });
        }

        [HttpDelete("expenses/{ExpenseId}")]
        [ProducesResponseType(typeof(DeleteExpenseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteExpenseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteExpenseByIdApiResponseContract>> DeleteExpenseById( [FromHeader] DeleteExpenseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deleteExpenseById = await _mediator.Send(new DeleteExpenseByIdCommand(contract.ExpenseID), cancellationToken);

            if (!deleteExpenseById.Success)
            {
                return BadRequest(new DeleteExpenseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deleteExpenseById.ReturnPath,
                    Messages = deleteExpenseById.Messages?.ToList(),
                });
            }

            return Ok(new DeleteExpenseByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = deleteExpenseById.Messages?.ToList(),
                ReturnPath = deleteExpenseById.ReturnPath
            });
        }

        [HttpPost("expenses")]
        [ProducesResponseType(typeof(CreateExpenseApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateExpenseApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateExpenseApiResponseContract>> CreateExpense( [FromQuery] CreateExpenseApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createExpense = await _mediator.Send(new CreateExpenseCommand(new ExpenseContract()
                                                                {
                                                                    Amount = contract.Amount
                                                                }), cancellationToken);

            if (!createExpense.Success)
            {
                return BadRequest(new CreateExpenseApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createExpense.ReturnPath,
                    Messages = createExpense.Messages?.ToList(),
                });
            }

            return Ok(new CreateExpenseApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = createExpense.Messages?.ToList(),
                ReturnPath = createExpense.ReturnPath
            });
        }

        [HttpPut("expenses/{ExpenseId}")]
        [ProducesResponseType(typeof(UpdateExpenseByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateExpenseByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateExpenseByIdApiResponseContract>> UpdateExpenseById([FromRoute]int ExpenseID, [FromQuery] UpdateExpenseByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateExpenseById = await _mediator.Send(new UpdateExpenseByIdCommand(new ExpenseContract()
                                                                {
                                                                    ExpenseID = ExpenseID,
                                                                    Amount = contract.Amount
                                                                }), cancellationToken);

            if (!updateExpenseById.Success)
            {
                return BadRequest(new UpdateExpenseByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateExpenseById.ReturnPath,
                    Messages = updateExpenseById.Messages?.ToList(),
                });
            }

            return Ok(new UpdateExpenseByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateExpenseById.Messages?.ToList(),
                ReturnPath = updateExpenseById.ReturnPath
            });
        }

    }
}