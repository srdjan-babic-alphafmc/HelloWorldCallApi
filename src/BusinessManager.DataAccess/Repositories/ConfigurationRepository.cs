using BusinessManager.DataAccess.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Data;
using BusinessManagerApi.Data.Repository;
using System;
using System.Linq;

namespace BusinessManager.DataAccess.Repositories
{
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(ApplicationDbContext ctx) : base(ctx)
        {

        }

        public bool DeleteConfiguration(Guid id)
        {
            var config = _context.Configuration.FirstOrDefault(x => x.Id.Equals(id));

            config.Deleted = true;
            _context.SaveChanges();

            return true;
        }

        public void UpdateConfiguration(Guid id, Configuration config)
        {
            var configToUpdate = _context.Configuration.FirstOrDefault(x => x.Id.Equals(id));

            configToUpdate.Name = config.Name;
            configToUpdate.TotalPrice = config.TotalPrice;
            configToUpdate.WhoOrdered = config.WhoOrdered;
            configToUpdate.Components = config.Components;

            _context.SaveChanges();
        }
    }
}
