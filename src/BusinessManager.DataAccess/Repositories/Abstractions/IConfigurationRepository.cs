using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.Abstractions
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        bool DeleteConfiguration(Guid id);
        void UpdateConfiguration(Guid id, Configuration config);
    }
}
