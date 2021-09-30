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

        [HttpGet("guests")]
        [ProducesResponseType(typeof(GetGuestsApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetGuestsApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetGuestsApiResponseContract>> GetGuests( [FromQuery] GetGuestsApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getGuests = await _mediator.Send(new GetGuestsCommand(), cancellationToken);

            if (!getGuests.Success)
            {
                return BadRequest(new GetGuestsApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getGuests.ReturnPath,
                    Messages = getGuests.Messages?.ToList(),
                });
            }

            return Ok(new GetGuestsApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getGuests.Messages?.ToList(),
                Result = getGuests.CompanyContracts,
                ReturnPath = getGuests.ReturnPath
            });
        }

        [HttpGet("companies/entered")]
        [ProducesResponseType(typeof(GetEnteredCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetEnteredCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetEnteredCompaniesApiResponseContract>> GetEnteredCompanies( [FromQuery] GetEnteredCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetEnteredCompaniesCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetEnteredCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetEnteredCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.CompanyContracts,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/entered/count")]
        [ProducesResponseType(typeof(GetNumberOfEnteredCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfEnteredCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfEnteredCompaniesApiResponseContract>> GetNumberOfEnteredCompanies( [FromQuery] GetNumberOfEnteredCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetNumberOfEnteredCompaniesCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetNumberOfEnteredCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfEnteredCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.NumberOfEnteredCompanies,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/entered/no-purchase")]
        [ProducesResponseType(typeof(GetEnteredCompaniesWithoutPurchaseApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetEnteredCompaniesWithoutPurchaseApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetEnteredCompaniesWithoutPurchaseApiResponseContract>> GetEnteredWithoutPurchaseCompanies( [FromQuery] GetEnteredCompaniesWithoutPurchaseApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetEnteredCompaniesWithoutPurchaseCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetEnteredCompaniesWithoutPurchaseApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetEnteredCompaniesWithoutPurchaseApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.CompanyContracts,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/entered/no-purchase/count")]
        [ProducesResponseType(typeof(GetNumberOfEnteredCompaniesWithoutPurchaseApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfEnteredCompaniesWithoutPurchaseApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfEnteredCompaniesWithoutPurchaseApiResponseContract>> GetNumberOfEnteredWithoutPurchaseCompanies( [FromQuery] GetNumberOfEnteredCompaniesWithoutPurchaseApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetNumberOfEnteredCompaniesWithoutPurchaseCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetNumberOfEnteredCompaniesWithoutPurchaseApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfEnteredCompaniesWithoutPurchaseApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.NumberOfEnteredCompaniesWithoutPurchase,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/not-entered")]
        [ProducesResponseType(typeof(GetNotEnteredCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNotEnteredCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNotEnteredCompaniesApiResponseContract>> GetNotEnteredCompanies( [FromQuery] GetNotEnteredCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetNotEnteredCompaniesCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetNotEnteredCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetNotEnteredCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.CompanyContracts,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/not-entered/count")]
        [ProducesResponseType(typeof(GetNumberOfNotEnteredCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfNotEnteredCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfNotEnteredCompaniesApiResponseContract>> GetNumberOfNotEnteredCompanies( [FromQuery] GetNumberOfNotEnteredCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanies = await _mediator.Send(new GetNumberOfNotEnteredCompaniesCommand(), cancellationToken);

            if (!getCompanies.Success)
            {
                return BadRequest(new GetNumberOfNotEnteredCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanies.ReturnPath,
                    Messages = getCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfNotEnteredCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanies.Messages?.ToList(),
                Result = getCompanies.NumberOfNotEnteredCompanies,
                ReturnPath = getCompanies.ReturnPath
            });
        }

        [HttpGet("companies/endorsement")]
        [ProducesResponseType(typeof(GetTotalEndorsementApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetTotalEndorsementApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetTotalEndorsementApiResponseContract>> GetTotalEndorsement( [FromQuery] GetTotalEndorsementApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getTotalEndorsement = await _mediator.Send(new GetTotalEndorsementCommand(), cancellationToken);

            if (!getTotalEndorsement.Success)
            {
                return BadRequest(new GetTotalEndorsementApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getTotalEndorsement.ReturnPath,
                    Messages = getTotalEndorsement.Messages?.ToList(),
                });
            }

            return Ok(new GetTotalEndorsementApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getTotalEndorsement.Messages?.ToList(),
                Result = getTotalEndorsement.TotalEndorsement,
                ReturnPath = getTotalEndorsement.ReturnPath
            });
        }
        

        [HttpGet("companies/count")]
        [ProducesResponseType(typeof(GetNumberOfCompaniesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfCompaniesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfCompaniesApiResponseContract>> GetNumberOfCompanies( [FromQuery] GetNumberOfCompaniesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getNumberOfCompanies = await _mediator.Send(new GetNumberOfCompaniesCommand(), cancellationToken);

            if (!getNumberOfCompanies.Success)
            {
                return BadRequest(new GetNumberOfCompaniesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getNumberOfCompanies.ReturnPath,
                    Messages = getNumberOfCompanies.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfCompaniesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getNumberOfCompanies.Messages?.ToList(),
                Result = getNumberOfCompanies.NumberOfCompanies,
                ReturnPath = getNumberOfCompanies.ReturnPath
            });
        }

        [HttpGet("guests/count")]
        [ProducesResponseType(typeof(GetNumberOfGuestsApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetNumberOfGuestsApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetNumberOfGuestsApiResponseContract>> GetNumberOfGuests( [FromQuery] GetNumberOfGuestsApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getNumberOfGuests = await _mediator.Send(new GetNumberOfGuestsCommand(), cancellationToken);

            if (!getNumberOfGuests.Success)
            {
                return BadRequest(new GetNumberOfGuestsApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getNumberOfGuests.ReturnPath,
                    Messages = getNumberOfGuests.Messages?.ToList(),
                });
            }

            return Ok(new GetNumberOfGuestsApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getNumberOfGuests.Messages?.ToList(),
                Result = getNumberOfGuests.NumberOfGuests,
                ReturnPath = getNumberOfGuests.ReturnPath
            });
        }

        [HttpGet("company-names")]
        [ProducesResponseType(typeof(GetCompanyNamesApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCompanyNamesApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCompanyNamesApiResponseContract>> GetCompanyNames( [FromQuery] GetCompanyNamesApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanyNames = await _mediator.Send(new GetCompanyNamesCommand(), cancellationToken);

            if (!getCompanyNames.Success)
            {
                return BadRequest(new GetCompanyNamesApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanyNames.ReturnPath,
                    Messages = getCompanyNames.Messages?.ToList(),
                });
            }

            return Ok(new GetCompanyNamesApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanyNames.Messages?.ToList(),
                Result = getCompanyNames.CompanyNames,
                ReturnPath = getCompanyNames.ReturnPath
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
                                                                    IsGuest = contract.IsGuest.ToLower() == "true" ? true : false,
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
        public async Task<ActionResult<UpdateCompanyByIdApiResponseContract>> UpdateCompanyById([FromRoute]int CompanyID, [FromQuery] UpdateCompanyByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var updateCompanyById = await _mediator.Send(new UpdateCompanyByIdCommand(new CompanyContract()
                                                                {
                                                                    CompanyID = CompanyID,
                                                                    CompanyName = contract.CompanyName,
                                                                    EMail = contract.EMail,
                                                                    Phone = contract.Phone,
                                                                    Endorsement = contract.Endorsement,
                                                                    IsGuest = contract.IsGuest.ToLower() == "true" ? true: false,
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

        [HttpGet("companies/id")]
        [ProducesResponseType(typeof(GetCompanyNameByIdApiResponseContract), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCompanyNameByIdApiResponseContract), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetCompanyNameByIdApiResponseContract>> GetCompanyIdByName( [FromQuery] GetCompanyNameByIdApiRequestContract contract, CancellationToken cancellationToken)
        {
            var getCompanyNameById = await _mediator.Send(new GetCompanyNameByIdCommand(contract.CompanyID), cancellationToken);

            if (!getCompanyNameById.Success)
            {
                return BadRequest(new GetCompanyNameByIdApiResponseContract()
                {
                    Instance = Guid.NewGuid().ToString(),
                    ReturnPath = getCompanyNameById.ReturnPath,
                    Messages = getCompanyNameById.Messages?.ToList(),
                });
            }

            return Ok(new GetCompanyNameByIdApiResponseContract
            {
                Instance = Guid.NewGuid().ToString(),
                Messages = getCompanyNameById.Messages?.ToList(),
                Result = getCompanyNameById.CompanyName,
                ReturnPath = getCompanyNameById.ReturnPath
            });
        }
    }
}