using System;

namespace Module.Network
{
    public interface INetworkGateway
    {
        void Request(NetworkAction networkRequest, Action successCallback = null);
    }
}