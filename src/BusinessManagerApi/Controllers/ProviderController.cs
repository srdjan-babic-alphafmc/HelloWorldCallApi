using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.Models;
using BusinessManager.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BusinessManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ProviderBusinessLogic _providerBusinessLogic;
        private readonly ILogger<ProviderController> _logger;
        private readonly IMapper _mapper;

        public ProviderController(IUnitOfWork unitOfWork, ILogger<ProviderController> log, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = log;
            _mapper = mapper;
            _providerBusinessLogic = new ProviderBusinessLogic(_unitOfWork, _logger, mapper);
        }

        /// <summary>
        /// Insert provider into database.
        /// </summary>
        /// <param name="provider"></param>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Bad request</response>
        [Route("CreateProvider")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProvider([FromBody] Provider provider)
        {
            try
            {
                if (provider == null)
                {
                    _logger.LogError("Provider object sent from provider is null.");
                    return BadRequest("Provider object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid provider object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var providerFromDb = await _providerBusinessLogic.CreateProvider(provider);

                _logger.LogInformation($"Provider successfully created");

                return CreatedAtRoute("GetProviderById", new { id = providerFromDb.Id }, providerFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProvider action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all providers from database.
        /// </summary>
        /// <param name=""></param>
        /// <response code="200">Returns the list of providers</response>
        /// <response code="400">Bad request</response>
        [Route("GetAllProviders")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllProviders()
        {
            try
            {
                var providers = await _providerBusinessLogic.GetAllProviders();

                _logger.LogInformation($"Returned all providers from database.");

                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProviders action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get provider by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return provider by id</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetProviderById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProviderById(Guid id)
        {
            try
            {
                var provider = await _providerBusinessLogic.GetProviderById(id);

                if (provider == null)
                {
                    _logger.LogError($"Provider with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned provider with id: {id}");
                    return Ok(provider);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProviderById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Soft delete provider in database.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SoftDeleteProvider(Guid id)
        {
            try
            {
                await _providerBusinessLogic.SoftDeleteProvider(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SoftDeleteProvider action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update provider row in database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="provider"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProvider(Guid id, [FromBody] Provider provider)
        {
            try
            {
                if (provider == null)
                {
                    _logger.LogError("Provider object sent from client is null.");
                    return BadRequest("Provider object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid provider object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _providerBusinessLogic.UpdateProvider(id, provider);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProvider action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
