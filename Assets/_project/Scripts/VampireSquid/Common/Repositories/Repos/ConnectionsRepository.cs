using System;
using VampireSquid.Common.Connections;

namespace VampireSquid.Common.Repositories.Repos
{
    public interface IClientsRepository : IRepository<Connection>
    {
        public Connection LocalOwner { get; }
    }
    
    [Serializable]
    public sealed class ConnectionsRepository : Repository<Connection>, IClientsRepository
    {
        public Connection LocalOwner { get; private set; }

        protected override bool OnlyUnique => true;

        protected override void OnAdded(Connection c)
        {
            if (c.IsOwner) LocalOwner = c;
        }

        protected override void OnRemoved(Connection c)
        {
            if (LocalOwner == c) LocalOwner = null;
        }
    }
}