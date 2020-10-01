using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.Abstractions
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        bool DeleteSale(Guid id);
        void UpdateSale(Guid id, Sale sale);
    }
}
