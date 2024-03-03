using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRepository
{
    public class HealthRepository : IHealthRepository
    {
        private DbContextOptions<DbContext> _options;
        public HealthRepository() 
        {
            _options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("Health").Options;
        }
        public Health GetHealth(string service)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Healths.Where(c => c.ServiceName == service).FirstOrDefault();
            }
        }

        public void PostHealth(Health health)
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient))
            {
                _ = context.Add(health);
                context.SaveChanges();
            }
        }
    }
}
