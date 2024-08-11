using Core;

namespace Module.Network
{
    public static class NetworkMessages
    {
        public struct ConnectionStatusChangedMessage : IMessage
        {
            public readonly bool Connected;

            public ConnectionStatusChangedMessage(bool connected)
            {
                Connected = connected;
            }
        }
    }
}