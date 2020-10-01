using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.DtoModels;
using BusinessManager.Models.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BusinessManager.Shared.BusinessLogic
{
    public class ConfigurationBusinessLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ConfigurationBusinessLogic(IUnitOfWork unitOfWork, ILogger logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ConfigurationDto> CreateConfiguration(Configuration config)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Configuration.Add(config);
                _unitOfWork.Complete();

                var configurationDto = _mapper.Map<Configuration, ConfigurationDto>(config);

                return configurationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<ConfigurationDto>> GetAllConfigurations()
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var configs = _unitOfWork.Configuration.GetAll()
                    .Where(x => x.Deleted.Equals(false))
                    .OrderBy(o => o.Name)
                    .ToList();

                var configurationDto = _mapper.Map<List<Configuration>, List<ConfigurationDto>>(configs);

                return configurationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<ConfigurationDto> GetConfigurationById(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var config = _unitOfWork.Configuration.GetById(id);

                var configurationDto = _mapper.Map<Configuration, ConfigurationDto>(config);

                return configurationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public async Task<bool> SoftDeleteConfiguration(Guid id)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                var isDeleted = _unitOfWork.Configuration.DeleteConfiguration(id);

                return isDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }

        public void UpdateConfiguration(Guid id, Configuration config)
        {
            try
            {
                _logger.LogInformation($"Envoke method {MethodBase.GetCurrentMethod().Name}");

                _unitOfWork.Configuration.UpdateConfiguration(id, config);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in {GetType().FullName} in method {MethodBase.GetCurrentMethod().Name}", ex);
                throw;
            }
        }
    }
}
