﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Health
    {
        public Guid? Id { get; set; }
        public required string ServiceName { get; set; }
        public required HealthStatus Status { get; set; }
        public required DateTime Timestamp { get; set; }
    }

    public enum HealthStatus
    {
        Healthy,
        Sick,
        Dying,
        Unknown
    }
}
