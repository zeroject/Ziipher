﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApplication.DependencyResolver
{
    public class DependencyResolverService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IHealthService, HealthService>();
        }
    }
}
