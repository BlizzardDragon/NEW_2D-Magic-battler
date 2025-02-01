using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using VampireSquid.Common.BeautifulLogs;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace VampireSquid.Common.Connections
{
    public class Connection : NetworkBehaviour
    {
#if ODIN_INSPECTOR
        [ReadOnly] 
#endif
        public NetworkedPresence presence = NetworkedPresence.Server;

        private readonly SyncVar<string> identification = new();
        private readonly SyncVar<bool>   runsHost       = new();

        public string Identification => identification.Value;
        public bool   RunsHost       => runsHost.Value;
        
        public override void OnStartNetwork()
        {
            base.OnStartNetwork();

            var id = $"Client {Owner.ClientId}";
            BLog.LogImportant($"Connection started for ID: [{OwnerId}] - {id}");
            
            identification.OnChange += OnIDChanged;
            
            if (Owner.IsLocalClient)
                UpdateInfo_ServerRpc(id, Owner.IsHost);         
            
            gameObject.name = id;
        }

        public override void OnStopClient()
        {
            base.OnStopServer();
            
            identification.OnChange -= OnIDChanged;

            BLog.LogImportant($"Connection ended [ID: {OwnerId}]");
        }

        public NetworkedPresence GetPresence()
        {
            var p = NetworkedPresence.Undefined;

            if (InstanceFinder.IsServerStarted)
                p |= NetworkedPresence.Server;

            if (Owner.IsHost)
                p |= NetworkedPresence.Host;

            if (Owner.IsLocalClient)
                p  |= NetworkedPresence.Local;
            else p |= NetworkedPresence.Remote;

            return p;
        }

        [ServerRpc] private void UpdateInfo_ServerRpc(string id, bool isHost)
        {
            identification.Value = id;
            runsHost.Value       = isHost;
        }
        
        private void OnIDChanged(string prev, string next, bool asServer) => 
            gameObject.name = Identification;
    }
}