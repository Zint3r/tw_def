using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Network
{
    public class NetworkGateway : INetworkGateway
    {
        private readonly Dictionary<string, string> defaultHttpHeaderProtobuf = new Dictionary<string, string>
        {
            {"Content-Type", "application/x-protobuf"},
            {"Accept", "application/x-protobuf"},
            //{"X-ClientVersion", "0.0.0"}
            {"X-ClientVersion", Application.version}
        };
        
        // [Inject]
        private NetworkModel networkModel;
        
        private readonly List<NetworkCall> requestsQueue = new List<NetworkCall>();
        
        private bool requestInProgress;
        
        public void Request(NetworkAction networkRequest, Action successCallback = null)
        {
            // requestsQueue.Add(networkRequest);
            // WorkOnQueue();
        }
        
        private void WorkOnQueue()
        {
        }
    }
}