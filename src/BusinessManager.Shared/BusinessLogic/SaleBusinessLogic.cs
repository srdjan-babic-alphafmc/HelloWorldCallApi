using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Shared.BusinessLogic
{
    public class SaleBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public SaleBusinessLogic(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SaleDto> CreateSale(Sale sale)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Sale.Add(sale);
                _unitOfWork.Complete();

                var saleDto = _mapper.Map<Sale, SaleDto>(sale);

                return saleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<SaleDto>> GetAllSales()
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var sales = _unitOfWork.Sale.GetAll()
                    .Where(x => x.Deleted.Equals(false))
                    .OrderBy(o => o.Buyer)
                    .ToList();

                var salesDto = _mapper.Map<List<Sale>, List<SaleDto>>(sales);

                return salesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<SaleDto> GetSaleById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var sale = _unitOfWork.Sale.GetById(id);

                var saleDto = _mapper.Map<Sale, SaleDto>(sale);

                return saleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<bool> SoftDeleteSale(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var isDeleted = _unitOfWork.Sale.DeleteSale(id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public void UpdateSale(Guid id, Sale sale)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Sale.UpdateSale(id, sale);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }
    }
}
