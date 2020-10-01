using BusinessManager.DataAccess.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessManager.DataAccess.Repositories
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }
        public bool DeleteProvider(Guid id)
        {
            var provider = _context.Provider.FirstOrDefault(x => x.Id.Equals(id));

            provider.Deleted = true;
            _context.SaveChanges();

            return true;
        }

        public void UpdateProvider(Guid id, Provider provider)
        {
            var providerToUpdate = _context.Provider.FirstOrDefault(x => x.Id.Equals(id));

            providerToUpdate.Name = provider.Name;
            providerToUpdate.Address = provider.Address;
            providerToUpdate.City = provider.City;
            providerToUpdate.PostalCode = provider.PostalCode;
            providerToUpdate.PhoneNumber = provider.PhoneNumber;
            providerToUpdate.Email = provider.Email;
            providerToUpdate.PIB = provider.PIB;
            providerToUpdate.Note = provider.Note;

            _context.SaveChanges();
        }
    }
}
