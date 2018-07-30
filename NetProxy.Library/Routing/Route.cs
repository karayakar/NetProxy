using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetProxy.Library.Routing
{
    public class Route
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public BindingProtocal BindingProtocal { get; set; }

        public TrafficType TrafficType { get; set; }
        public bool AutoStart { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ListenPort { get; set; }
        public int SpinLockCount { get; set; }
        public bool UseStickySessions { get; set; }
        public bool CompressBindingProxy { get; set; }
        public bool CompressEndpointProxy { get; set; }
        public bool EncryptBindingProxy { get; set; }
        public bool EncryptEndpointProxy { get; set; }
        public string BindingPreSharedKey { get; set; }
        public string EndpointPreSharedKey { get; set; }
        public bool BindingIsProxy { get; set; }
        public bool EndpointIsProxy { get; set; }
        public int AcceptBacklogSize { get; set; }
        public int EncryptionInitilizationTimeoutMs { get; set; }
        public int InitialBufferSize { get; set; }
        public int StickySessionCacheExpiration { get; set; }
        public int MaxBufferSize { get; set; }
        public string Description { get; set; }
        public List<Binding> Bindings { get; set; }
        public bool ListenOnAllAddresses { get; set; }

        public Endpoints Endpoints = new Endpoints();

        public HttpHeaderRules HttpHeaderRules = new HttpHeaderRules();

        public Route()
        {
            Id = Guid.NewGuid();
            AcceptBacklogSize = Constants.DefaultAcceptBacklogSize;
            InitialBufferSize = Constants.DefaultInitialBufferSize;
            MaxBufferSize = Constants.DefaultMaxBufferSize;
            StickySessionCacheExpiration = Constants.DefaultStickySessionExpiration;
            SpinLockCount = Constants.DefaultSpinLockCount;
            EncryptionInitilizationTimeoutMs = Constants.DefaultEncryptionInitilizationTimeoutMs;
            Bindings = new List<Binding>();
        }
    }
}