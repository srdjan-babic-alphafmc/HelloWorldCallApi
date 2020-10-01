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
    public class SaleController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SaleController> _logger;
        private readonly IMapper _mapper;
        private SaleBusinessLogic _saleBusinessLogic;

        public SaleController(IUnitOfWork unitOfWork, ILogger<SaleController> log, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = log;
            _mapper = mapper;
            _saleBusinessLogic = new SaleBusinessLogic(_unitOfWork, _logger, _mapper);
        }

        /// <summary>
        /// Insert sale into database.
        /// </summary>
        /// <param name="sale"></param>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Bad request</response>
        [Route("CreateSale")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            try
            {
                if (sale == null)
                {
                    _logger.LogError("Sale object sent from client is null.");
                    return BadRequest("Sale object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid sale object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var saleFromDb = await _saleBusinessLogic.CreateSale(sale);

                _logger.LogInformation($"Sale successfully created");

                return CreatedAtRoute("GetSaleById", new { id = saleFromDb.Id }, saleFromDb);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateSale action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all sales from database.
        /// </summary>
        /// <param name=""></param>
        /// <response code="200">Returns the list of sales</response>
        /// <response code="400">Bad request</response>
        [Route("GetAllSales")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var sales = await _saleBusinessLogic.GetAllSales();

                _logger.LogInformation($"Returned all sales from database.");

                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSales action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get sale by id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return sale by id</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetSaleById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            try
            {
                var sale = await _saleBusinessLogic.GetSaleById(id);

                if (sale == null)
                {
                    _logger.LogError($"Sale with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned sale with id: {id}");
                    return Ok(sale);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSaleById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Soft delete sale in database.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SoftDeleteSale(Guid id)
        {
            try
            {
                await _saleBusinessLogic.SoftDeleteSale(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SoftDeleteSale action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update sale row in database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sale"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] Sale sale)
        {
            try
            {
                if (sale == null)
                {
                    _logger.LogError("Sale object sent from client is null.");
                    return BadRequest("Sale object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Sale object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _saleBusinessLogic.UpdateSale(id, sale);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSale action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
