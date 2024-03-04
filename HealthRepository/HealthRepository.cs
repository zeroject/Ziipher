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
        public List<Health> GetHealth()
        {
            using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
            {
                return context.Healths.ToList();
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
