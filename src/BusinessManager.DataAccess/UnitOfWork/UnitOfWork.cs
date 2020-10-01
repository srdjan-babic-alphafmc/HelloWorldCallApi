using BusinessManager.DataAccess.Abstractions;
using BusinessManager.DataAccess.Repositories;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManagerApi.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductsRepository(_context);
            Clients = new ClientsRepository(_context);
            Configuration = new ConfigurationRepository(_context);
            Provider = new ProviderRepository(_context);
            Sale = new SaleRepository(_context);
        }

        public IProductsRepository Products { get; private set; }

        public IClientsRepository Clients { get; private set; }

        public IConfigurationRepository Configuration { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public ISaleRepository Sale { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
