using AutoMapper;
using BusinessManager.DataAccess.Repositories;
using BusinessManager.DataAccess.UnitOfWork;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Shared.BusinessLogic
{
    public class ProductsBusinessLogic
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductsBusinessLogic(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductsDto> CreateProduct(Products product)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();

                var clientDto = _mapper.Map<Products, ProductsDto>(product);

                return clientDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ProductsDto>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var products = _unitOfWork.Products.GetAll()
                    .Where(x=> x.Deleted.Equals(false))
                    .ToList();
                _unitOfWork.Complete();

                var clientDto = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductsDto>>(products);

                return clientDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<ProductsDto> GetProductById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var product = _unitOfWork.Products.GetById(id);
                _unitOfWork.Complete();

                var productDto = _mapper.Map<Products, ProductsDto>(product);

                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<bool> SoftDeleteProduct(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var isDeleted = _unitOfWork.Products.DeleteProduct(id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public void UpdateProduct(Guid id, Products product)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Products.UpdateProduct(id, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }
    }
}
