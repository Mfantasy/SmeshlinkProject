using System;

namespace SmeshLink.SmeshServer
{
    /// <summary>
    /// Defines global constants.
    /// </summary>
    public static class SmeshDefine
    {
        /// <summary>
        /// Default MTU of serial packets.
        /// </summary>
        public const Int32 SerialPacketMtu = 256;
        /// <summary>
        /// Default retry interval of socket forwarder.
        /// </summary>
        public const Int32 SocketRetryConnectingInterval = 10000;
        /// <summary>
        /// Default length of header in XML forwarding.
        /// </summary>
        public const Int32 ForwardingHeaderLength = 4;
        //public const Int32 ForwardingHeaderLength = 2;
        /// <summary>
        /// Default size of buffer in XML forwarding.
        /// </summary>
        public const Int32 ForwardingBufferSize = 5000;
        /// <summary>
        /// Default port of server forwarder.
        /// </summary>
        public const Int32 ForwarderListeningPort = 9005;
        /// <summary>
        /// Default backlog of server forwarder.
        /// </summary>
        public const Int32 ForwarderListerningBacklog = 100;
        /// <summary>
        /// Default length of header in serial forwarder.
        /// </summary>
        public const Int32 SerialForwarderHeaderLength = 1;

        /// <summary>
        /// Message that the number of active connections reaches the upper bound.
        /// </summary>
        public static readonly Byte[] TOO_MANY_CONNECTIONS = System.Text.Encoding.UTF8.GetBytes("Oops! Seems to get a traffic jam here :(");
        /// <summary>
        /// Message that the handshake fails.
        /// </summary>
        public static readonly Byte[] INCOMPATIBLE_PROTOCOL = System.Text.Encoding.UTF8.GetBytes("Oops! Cannot understand what you said :(");
        /// <summary>
        /// Message that the connection is canceled.
        /// </summary>
        public static readonly Byte[] CONNECTION_CANCEL = System.Text.Encoding.UTF8.GetBytes("Oops! This conversation has been canceled.");

        public enum HDLCPacketType
        {
            XPACKET_ACK = 0x40,
            XPACKET_W_ACK = 0x41,
            XPACKET_NO_ACK = 0x42,

            XPACKET_ESC = 0x7D,    //!< Reserved for serial packetizer escape code.
            XPACKET_START = 0x7E,    //!< Reserved for serial packetizer start code.
            XPACKET_TEXT_MSG = 0xF8,    //!< Special id for sending text error messages.
        }
    }
}