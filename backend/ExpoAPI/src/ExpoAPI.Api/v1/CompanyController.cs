using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpoAPI.Api.Controllers.Assemblers;
using ExpoAPI.Api.Controllers.Contracts.Requests;
using ExpoAPI.Api.Controllers.Contracts.Responses;
// using ExpoAPI.Domain.Commands;
// using ExpoAPI.Domain.Contracts;


namespace ExpoAPI.Api.Controllers
{
    public class CompanyController: ControllerBase
    {
        ILogger<CompanyController> _logger;
        IMediator _mediator;
        ITestAssembler _assembler;
        public CompanyController(ILogger<CompanyController> logger, IMediator mediator, ITestAssembler assembler)
        {
            _logger = logger;
            _mediator = mediator;
            _assembler = assembler;
        }

        [HttpGet("api/test-endpoint")]
        [ProducesResponseType(typeof(ApiTestResponseContract), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiTestResponseContract), (int)HttpStatusCode.BadRequest)]
        public async void TestEndpoint([FromBody] ApiTestRequestContract request, CancellationToken cancellationToken)
        {
            var command = _assembler.ToCommand(request);
            // return Created("api/v1/host", _addHostItemAssembler.ToResponse(await _mediator.Send(command)));
 
        }
    }
}