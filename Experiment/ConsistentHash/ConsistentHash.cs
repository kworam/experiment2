using System;

namespace Experiment
{
    public interface ConsistentHash
    {
        void AddServer(int serverId);

        void RemoveServer(int serverId);

        int GetServerForClient(Guid clientId);
    }
}
