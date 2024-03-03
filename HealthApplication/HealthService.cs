using Domain;
using HealthRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApplication
{
    public class HealthService : IHealthService
    {
        IHealthRepository _healthRepository;
        public HealthService(IHealthRepository healthRepository)
        {
            _healthRepository = healthRepository;
        }

        public Health GetHealth(string service)
        {
            return _healthRepository.GetHealth(service);
        }

        public void PostHealth(Health health)
        {
            _healthRepository.PostHealth(health);
        }
    }
}
