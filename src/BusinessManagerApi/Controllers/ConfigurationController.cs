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
    public class ConfigurationController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IMapper _mapper;
        private ConfigurationBusinessLogic _configurationBusinessLogic;

        public ConfigurationController(IUnitOfWork unitOfWork, ILogger<ConfigurationController> log, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = log;
            _mapper = mapper;
            _configurationBusinessLogic = new ConfigurationBusinessLogic(_unitOfWork, _logger, _mapper);
        }

        /// <summary>
        /// Insert configuration into database.
        /// </summary>
        /// <param name="config"></param>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Bad request</response>
        [Route("CreateConfiguration")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateConfiguration([FromBody] Configuration config)
        {
            try
            {
                if (config == null)
                {
                    _logger.LogError("Configuration object sent from config is null.");
                    return BadRequest("Configuration object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Configuration object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var configFromDb = await _configurationBusinessLogic.CreateConfiguration(config);

                _logger.LogInformation($"Configuration successfully created");

                return CreatedAtRoute("GetConfigurationById", new { id = configFromDb.Id }, configFromDb);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateConfiguration action: {ex.Message}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets all configurations from database.
        /// </summary>
        /// <param name=""></param>
        /// <response code="200">Returns the list of configs</response>
        /// <response code="400">Bad request</response>
        [Route("GetAllConfigurations")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllConfigurations()
        {
            try
            {
                var configs = await _configurationBusinessLogic.GetAllConfigurations();

                _logger.LogInformation($"Returned all configurations from database.");

                return Ok(configs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllConfigurations action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get configuration by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return configuration by id</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetConfigurationById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetConfigurationById(Guid id)
        {
            try
            {
                var configuration = await _configurationBusinessLogic.GetConfigurationById(id);

                if (configuration == null)
                {
                    _logger.LogError($"Configuration with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned Configuration with id: {id}");
                    return Ok(configuration);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetConfigurationById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Soft delete configuration in database.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SoftDeleteConfiguration(Guid id)
        {
            try
            {
                await _configurationBusinessLogic.SoftDeleteConfiguration(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SoftDeleteConfiguration action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update configuration row in database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="config"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateConfiguration(Guid id, [FromBody] Configuration config)
        {
            try
            {
                if (config == null)
                {
                    _logger.LogError("Configuration object sent from config is null.");
                    return BadRequest("Configuration object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Configuration object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _configurationBusinessLogic.UpdateConfiguration(id, config);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateConfiguration action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
