using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwaitHealthCheckTester
{
    public class HealthCheck
    {
        public string AppName { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
    }
}
