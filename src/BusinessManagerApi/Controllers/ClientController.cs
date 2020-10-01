using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.Models;
using BusinessManager.Shared.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusinessManagerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;
        private ClientsBusinessLogic _clientsBusinessLogic;

        public ClientController(IUnitOfWork unitOfWork, ILogger<ClientController> log, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = log;
            _mapper = mapper;
            _clientsBusinessLogic = new ClientsBusinessLogic(_unitOfWork, _logger, _mapper);
        }

        /// <summary>
        /// Insert client into database.
        /// </summary>
        /// <param name="client"></param>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Bad request</response>
        [Route("CreateClient")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateClient([FromBody] Clients client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogError("Client object sent from client is null.");
                    return BadRequest("Client object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid client object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var clientFromDb = await _clientsBusinessLogic.CreateClient(client);

                _logger.LogInformation($"Client successfully created");

                return CreatedAtRoute("GetClientById", new { id = clientFromDb.Id }, clientFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateClient action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all clients from database.
        /// </summary>
        /// <param name=""></param>
        /// <response code="200">Returns the list of clients</response>
        /// <response code="400">Bad request</response>
        [Route("GetAllClients")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientsBusinessLogic.GetAllClients();

                _logger.LogInformation($"Returned all clients from database.");

                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllClients action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get client by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return client by id</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetClientById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            try
            {
                var client = await _clientsBusinessLogic.GetClientById(id);

                if (client == null)
                {
                    _logger.LogError($"Client with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned client with id: {id}");
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetClientById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Soft delete client in database.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SoftDeleteClient(Guid id)
        {
            try
            {
                await _clientsBusinessLogic.SoftDeleteClient(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SoftDeleteClient action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update client row in database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] Clients client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogError("Client object sent from client is null.");
                    return BadRequest("Client object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Client object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _clientsBusinessLogic.UpdateClient(id, client);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateClient action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
