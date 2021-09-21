using ExpoAPI.Contracts.Responses;
using ExpoAPI.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ExpoAPI.UseCases.Company;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace ExpoAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("companies")]
        [ProducesResponseType(typeof(GetCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCompaniesApiResponseContract>> GetCompanies( [FromQuery] GetCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetCompaniesCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.CompanyContracts,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/{companyId}")]
        [ProducesResponseType(typeof(GetCompanyByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCompanyByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCompanyByIdApiResponseContract>> GetCompanyById( [FromHeader] GetCompanyByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanyById = await _mediator.Send(new GetCompanyByIdCommand(contract.CompanyID), cancellationToken);

            if (!getCompanyById.Success)
            {
                return BadRequest(new GetCompanyByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanyById.ReturnPath,
                    Messages = getCompanyById.Messages?.ToList(),
                });
            }

            return Ok(new GetCompanyByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanyById.Messages?.ToList(),
                Result = getCompanyById.CompanyContract,
                ReturnPath = getCompanyById.ReturnPath
            });
        }

        [HttpDelete("companies/{companyId}")]
        [ProducesResponseType(typeof(DeleteCompanyByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteCompanyByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DeleteCompanyByIdApiResponseContract>> DeleteCompanyById( [FromHeader] DeleteCompanyByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var deleteCompanyById = await _mediator.Send(new DeleteCompanyByIdCommand(contract.CompanyID), cancellationToken);

            if (!deleteCompanyById.Success)
            {
                return BadRequest(new DeleteCompanyByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = deleteCompanyById.ReturnPath,
                    Messages = deleteCompanyById.Messages?.ToList(),
                });
            }

            return Ok(new DeleteCompanyByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = deleteCompanyById.Messages?.ToList(),
                ReturnPath = deleteCompanyById.ReturnPath
            });
        }

        [HttpPost("companies")]
        [ProducesResponseType(typeof(CreateCompanyApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CreateCompanyApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCompanyApiResponseContract>> CreateCompany( [FromQuery] CreateCompanyApiRequestContract contract, CancellationToken cancellationToken)
        {
            var createCompany = await _mediator.Send(new CreateCompanyCommand(new CompanyContract()
                                                                {
                                                                    CompanyName = contract.CompanyName,
                                                                    EMail = contract.EMail,
                                                                    Phone = contract.Phone,
                                                                    Endorsement = contract.Endorsement,
                                                                    IsEntered = contract.IsEntered.ToLower() == "true" ? true: false
                                                                }), cancellationToken);

            if (!createCompany.Success)
            {
                return BadRequest(new CreateCompanyApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = createCompany.ReturnPath,
                    Messages = createCompany.Messages?.ToList(),
                });
            }

            return Ok(new CreateCompanyApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = createCompany.Messages?.ToList(),
                ReturnPath = createCompany.ReturnPath
            });
        }

        [HttpPut("companies/{companyId}")]
        [ProducesResponseType(typeof(UpdateCompanyByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UpdateCompanyByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateCompanyByIdApiResponseContract>> UpdateCompanyById( [FromQuery] UpdateCompanyByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateCompanyById = await _mediator.Send(new UpdateCompanyByIdCommand(new CompanyContract()
                                                                {
                                                                    CompanyID = contract.CompanyID,
                                                                    CompanyName = contract.CompanyName,
                                                                    EMail = contract.EMail,
                                                                    Phone = contract.Phone,
                                                                    Endorsement = contract.Endorsement,
                                                                    IsEntered = contract.IsEntered.ToLower() == "true" ? true: false
                                                                }), cancellationToken);

            if (!updateCompanyById.Success)
            {
                return BadRequest(new UpdateCompanyByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = updateCompanyById.ReturnPath,
                    Messages = updateCompanyById.Messages?.ToList(),
                });
            }

            return Ok(new UpdateCompanyByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = updateCompanyById.Messages?.ToList(),
                ReturnPath = updateCompanyById.ReturnPath
            });
        }

        [HttpGet("companies/name")]
        [ProducesResponseType(typeof(GetCompanyIdByNameApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCompanyIdByNameApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCompanyIdByNameApiResponseContract>> GetCompanyIdByName( [FromQuery] GetCompanyIdByNameApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanyByIdByName = await _mediator.Send(new GetCompanyIdByNameCommand(contract.CompanyName), cancellationToken);

            if (!getCompanyByIdByName.Success)
            {
                return BadRequest(new GetCompanyIdByNameApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanyByIdByName.ReturnPath,
                    Messages = getCompanyByIdByName.Messages?.ToList(),
                });
            }

            return Ok(new GetCompanyIdByNameApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanyByIdByName.Messages?.ToList(),
                Result = getCompanyByIdByName.CompanyID,
                ReturnPath = getCompanyByIdByName.ReturnPath
            });
        }
    }
}