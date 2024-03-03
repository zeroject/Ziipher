using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApplication
{
    public interface IHealthService
    {
        public Health GetHealth(string service);
        public void PostHealth(Health health);
    }
}
