using System.Net.Sockets;
using NetProxy.Hub.Common;
using NetProxy.Library.Routing;

namespace NetProxy.Service.Routing
{
    public class SocketState
    {
        /// <summary>
        /// the conection was made by a remote peer to the proxy server.
        /// </summary>
        public bool IsIncomming { get; set; }
        /// <summary>
        /// The connection was made by the proxy to a remote peer.
        /// </summary>
        public bool IsOutgoing { get; set; }
        public Route Route { get; set; }
        public SocketState Peer { get; set; }
        public int BytesReceived { get; set; }
        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }
        public byte[] PayloadBuilder;
        public SecureKeyExchange.SecureKeyNegotiator KeyNegotiator { get; set; }
        public bool IsEncryptionNegotationComplete { get; set; }
        public int PayloadBuilderLength { get; set; }
        public string HttpHeaderBuilder { get; set; }
        public int MaxBufferSize { get; set; }

        private bool _isProxyNegotationComplete = false;
        public bool IsProxyNegotationComplete
        {
            get
            {
                if (((this.IsIncomming && Route.BindingIsProxy == false) && (this.Peer.IsOutgoing && Route.EndpointIsProxy == false))
                    || ((this.IsOutgoing && Route.EndpointIsProxy == false) && (this.Peer.IsIncomming && Route.BindingIsProxy == false)))
                {
                    return true; //No Proxying.
                }
                else if (((this.IsOutgoing && Route.EndpointIsProxy) && (this.Peer.IsIncomming && Route.BindingIsProxy))
                    || ((this.IsIncomming && Route.BindingIsProxy) && (this.Peer.IsOutgoing && Route.EndpointIsProxy)))
                {
                    //Both connections are Proxy endpoints.
                    return _isProxyNegotationComplete && this.Peer._isProxyNegotationComplete;
                }
                else if (((this.IsOutgoing && Route.EndpointIsProxy) && (this.Peer.IsIncomming && Route.BindingIsProxy == false))
                    || ((this.IsIncomming && Route.BindingIsProxy) && (this.Peer.IsOutgoing && Route.EndpointIsProxy == false)))
                {
                    //Only the current connection is a Proxy.
                    return _isProxyNegotationComplete;
                }
                else if (((this.IsOutgoing && Route.EndpointIsProxy == false) && (this.Peer.IsIncomming && Route.BindingIsProxy))
                    || ((this.IsIncomming && Route.BindingIsProxy == false) && (this.Peer.IsOutgoing && Route.EndpointIsProxy)))
                {
                    //Only the peer connection is a Proxy.
                    return this.Peer._isProxyNegotationComplete;
                }

                //Seriously, shouldn't ever get here...
                return _isProxyNegotationComplete && this.Peer.IsProxyNegotationComplete;
            }
        }

        public void SetProxyNegotationComplete()
        {
            _isProxyNegotationComplete = true;
        }

        public bool UseEncryption
        {
            get
            {
                return (this.IsOutgoing && Route.EndpointIsProxy && Route.EncryptEndpointProxy)
                || (this.IsIncomming && Route.BindingIsProxy && Route.EncryptBindingProxy);
            }
        }

        public bool UseCompression
        {
            get
            {
                return (this.IsOutgoing && Route.EndpointIsProxy && Route.CompressEndpointProxy)
                || (this.IsIncomming && Route.BindingIsProxy && Route.CompressBindingProxy);
            }
        }

        public bool UsePackets
        {
            get
            {
                return (this.IsOutgoing && Route.EndpointIsProxy)
                            || (this.IsIncomming && Route.BindingIsProxy);
            }
        }

        public SocketState()
        {
            HttpHeaderBuilder = string.Empty;
            Buffer = new byte[Constants.DefaultBufferSize];
            PayloadBuilder = new byte[0];
        }

        public SocketState(Socket socket, int initialBufferSize)
        {
            HttpHeaderBuilder = string.Empty;
            Socket = socket;
            Buffer = new byte[initialBufferSize];
            PayloadBuilder = new byte[0];
        }
    }
}
