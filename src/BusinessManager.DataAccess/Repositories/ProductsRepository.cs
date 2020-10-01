using BusinessManager.DataAccess.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data;
using BusinessManagerApi.Data.Repository;
using System;
using System.Linq;

namespace BusinessManager.DataAccess.Repositories
{
    public class ProductsRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public bool DeleteProduct(Guid id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(id));

            product.Deleted = true;
            _context.SaveChanges();

            return true;
        }

        public void UpdateProduct(Guid id, Products product)
        {
            var productToUpdate = _context.Products.FirstOrDefault(x => x.Id.Equals(id));

            productToUpdate.Category = product.Category;
            productToUpdate.Barcode = product.Barcode;
            productToUpdate.SerialNumber = product.SerialNumber;
            productToUpdate.PurchasePrice = product.PurchasePrice;
            productToUpdate.PurchaseDate = product.PurchaseDate;
            productToUpdate.SalePrice = product.SalePrice;
            productToUpdate.Warranty = product.Warranty;
            productToUpdate.WarrantyExpirationDate = product.WarrantyExpirationDate;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Others = product.Others;

            _context.SaveChanges();
        }
    }
}
