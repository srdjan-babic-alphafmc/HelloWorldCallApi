using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.Abstractions
{
    public interface IProductsRepository : IGenericRepository<Products>
    {
        bool DeleteProduct(Guid id);
        void UpdateProduct(Guid id, Products product);
    }
}
