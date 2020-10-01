using BusinessManager.DataAccess.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data;
using BusinessManagerApi.Data.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessManager.DataAccess.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
        public bool DeleteSale(Guid id)
        {
            var sale = _context.Sale.FirstOrDefault(x => x.Id.Equals(id));

            sale.Deleted = true;
            _context.SaveChanges();

            return true;
        }

        public void UpdateSale(Guid id, Sale sale)
        {
            var saleToUpdate = _context.Sale.FirstOrDefault(x => x.Id.Equals(id));

            saleToUpdate.Buyer = sale.Buyer;
            saleToUpdate.Catalog = sale.Catalog;
            saleToUpdate.TotalPrice = sale.TotalPrice;
            saleToUpdate.SaleDate = sale.SaleDate;

            _context.SaveChanges();
        }
    }
}
