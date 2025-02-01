using System;

namespace VampireSquid.Common.Connections
{
    [Flags]
    public enum NetworkedPresence
    {
        Undefined = 0,
        Server    = 1,                           
        Host      = 2,
        Local     = 4,
        Remote    = 8,
    }

    public static class NetworkedPresenceExt
    {
        public static bool IsLocal(this NetworkedPresence presence) =>
            presence.HasFlag(NetworkedPresence.Local);

        public static bool IsRemote(this NetworkedPresence presence) =>
            presence.HasFlag(NetworkedPresence.Remote);

        public static bool OnServer(this NetworkedPresence presence) =>
            presence.HasFlag(NetworkedPresence.Server) || presence.HasFlag(NetworkedPresence.Host);
    }
}