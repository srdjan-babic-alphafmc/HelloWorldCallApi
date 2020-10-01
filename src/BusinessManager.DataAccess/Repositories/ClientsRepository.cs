using BusinessManager.DataAccess.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.DataAccess.Repositories
{
    public class ClientsRepository : GenericRepository<Clients>, IClientsRepository
    {
        public ClientsRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public bool DeleteClient(Guid id)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id.Equals(id));

            client.Deleted = true;
            _context.SaveChanges();

            return true;
        }

        public void UpdateClient(Guid id, Clients client)
        {
            var clientToUpdate = _context.Clients.FirstOrDefault(x => x.Id.Equals(id));

            clientToUpdate.Name = client.Name;
            clientToUpdate.Address = client.Address;
            clientToUpdate.City = client.City;
            clientToUpdate.PostalCode = client.PostalCode;
            clientToUpdate.PhoneNumber = client.PhoneNumber;
            clientToUpdate.Email = client.Email;
            clientToUpdate.PIB = client.PIB;
            clientToUpdate.Note = client.Note;

            _context.SaveChanges();
        }
    }
}
