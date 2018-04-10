﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxy.Library.Payloads
{
    public class RouteGridStats
    {
        public Guid Id { get; set; }
        public bool IsRunning { get; set; }
        public UInt64 TotalConnections { get; set; }
        public int CurrentConnections { get; set; }
        public UInt64 BytesSent { get; set; }
        public UInt64 BytesReceived { get; set; }
    }
}
