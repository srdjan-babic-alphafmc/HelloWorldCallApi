using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.Abstractions
{
    public interface IProviderRepository : IGenericRepository<Provider>
    {
        bool DeleteProvider(Guid id);
        void UpdateProvider(Guid id, Provider provider);
    }
}
