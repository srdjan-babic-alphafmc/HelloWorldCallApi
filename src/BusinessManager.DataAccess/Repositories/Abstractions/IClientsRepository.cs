using BusinessManager.Models.Models;
using BusinessManagerApi.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.DataAccess.Abstractions
{
    public interface IClientsRepository : IGenericRepository<Clients>
    {
        bool DeleteClient(Guid id);
        void UpdateClient(Guid id, Clients client);
    }
}
