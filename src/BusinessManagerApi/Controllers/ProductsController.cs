using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.Models;
using BusinessManager.Shared.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusinessManagerApi.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private ProductsBusinessLogic _productsBusinessLogic;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, ILogger<ProductsController> log, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = log;
            _mapper = mapper;
            _productsBusinessLogic = new ProductsBusinessLogic(_unitOfWork, _logger, mapper);
        }

        /// <summary>
        /// Insert product data to database.
        /// </summary>
        /// <param name="product"></param>
        /// <response code="201">Successfully created</response>
        /// <response code="400">Bad request</response>
        [Route("CreateProduct")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] Products product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogError("Product object sent from product is null.");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Product object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var productFromDb = await _productsBusinessLogic.CreateProduct(product);

                _logger.LogInformation($"Product successfully created");

                return CreatedAtRoute("GetProductById", new { id = productFromDb.Id }, productFromDb);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProduct action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all products from database.
        /// </summary>
        /// <returns>List of products</returns>
        /// <response code="200">Returns the list of items</response>
        /// <response code="400">Bad request</response>
        [Route("GetProducts")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productsBusinessLogic.GetAllProducts();

                _logger.LogInformation($"Returned all products from database.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProducts action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get product from db by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single product</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productsBusinessLogic.GetProductById(id);

                if (product == null)
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned product with id: {id}");
                    return Ok(product);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetProductById action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        /// <summary>
        /// Soft delete product in database.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SoftDeleteProduct(Guid id)
        {
            try
            {
                await _productsBusinessLogic.SoftDeleteProduct(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside SoftDeleteProduct action: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update product from database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="400">Bad request</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Products product)
        {
            try
            {
                if(product == null)
                {
                    _logger.LogError("Product object sent from product is null.");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Product object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _productsBusinessLogic.UpdateProduct(id, product);

                return NoContent();

            }
            catch (Exception exx)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action :{exx.Message}");
                return BadRequest(exx.Message);
            }
        }

        //public async Task<IActionResult> SearchProduct()
        //{
        //    return null;
        //}
    }
}
