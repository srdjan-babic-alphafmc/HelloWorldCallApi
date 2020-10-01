using BusinessManager.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManager.DataAccess.UnitOfWork.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IProductsRepository Products { get; }
        IClientsRepository Clients { get; }
        IConfigurationRepository Configuration { get; }
        IProviderRepository Provider { get; }
        ISaleRepository Sale { get; }
        int Complete();
    }
}
