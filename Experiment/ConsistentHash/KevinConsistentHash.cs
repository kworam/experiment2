using System;
using System.Collections.Generic;
using System.Text;

namespace Experiment
{
    public class KevinConsistentHash : ConsistentHash
    {
        private readonly Dictionary<int, Server> serverIdToServerMap = new Dictionary<int, Server>();
        private readonly Dictionary<Guid, ClientRecord> clientIdToClientRecordMap = new Dictionary<Guid, ClientRecord>();
        private readonly ServerReplica[] circularList;

        private readonly int maxNumServers;
        private readonly int numReplicasPerServer;
        private readonly int circularListLen;

		private class ClientRecord
		{
		    public int CircularIndex { get; private set; }

		    public ServerReplica ServerReplica { get; private set; }

		    public ClientRecord(int circularIndex, ServerReplica serverReplica)
			{
				this.CircularIndex = circularIndex;
				this.ServerReplica = serverReplica;
			}
		}

        private class Server
        {
            private readonly List<ServerReplica> serverReplicas = new List<ServerReplica>();

            public int ServerId { get; private set; }

            public Server(int serverId)
            {
                this.ServerId = serverId;
            }

            public void AddReplica(ServerReplica serverReplica)
            {
                this.serverReplicas.Add(serverReplica);
            }

            public IEnumerable<ServerReplica> GetReplicas()
            {
                return this.serverReplicas;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Server {0}", this.ServerId));
                sb.AppendLine(string.Format("Replicas:", this.ServerId));
                foreach (ServerReplica replica in this.GetReplicas())
                {
                    sb.Append(replica.ToString());
                    sb.AppendLine();
                }
                return sb.ToString();
            }
        }

        private class ServerReplica
        {
			private readonly List<Guid> clientIds = new List<Guid>();

			public int CircularListIndex { get; private set; }

            public Server Server { get; private set; }

			public Guid ReplicaId { get; private set; }	

			public ServerReplica(Guid replicaId, int circularListIndex, Server server)
            {
                this.ReplicaId = replicaId;
                this.CircularListIndex = circularListIndex;
                this.Server = server;
            }

            public int NumClients
            {
                get { return clientIds.Count; }
            }

			public void AddClient(Guid clientId)
			{
				if (clientIds.Contains(clientId))
				{
					throw new ArgumentException(
						string.Format("Server Replica {0} for Server {1} already contains client id {2}",
						this.ReplicaId,
						this.Server.ServerId,
						clientId));
				}

				clientIds.Add(clientId);
			}

            public void RemoveClient(Guid clientId)
            {
                clientIds.Remove(clientId);
            }

            public IEnumerable<Guid> GetCopyOfClients()
            {
                return new List<Guid>(this.clientIds);
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("Replica {0}", this.ReplicaId));
                sb.AppendLine(string.Format("Circular List Index {0}", this.CircularListIndex));

				foreach (Guid clientId in this.clientIds)
				{
					sb.AppendLine(string.Format("Client Id {0}", clientId));
				}

                return sb.ToString();
            }
        }

        public KevinConsistentHash(int maxNumServers, int numReplicasPerServer)
        {
            this.maxNumServers = maxNumServers;
            this.numReplicasPerServer = numReplicasPerServer;
            this.circularListLen = maxNumServers * numReplicasPerServer * 2;
            this.circularList = new ServerReplica[this.circularListLen];
        }

        public void AddServer(int serverId)
        {
            if (serverIdToServerMap.Count == this.maxNumServers)
            {
                throw new ArgumentException(String.Format(
                    "Cannot add server {0}, the collection already contains the maximum number of servers which is {1}."
                    , serverId,
                    this.maxNumServers));
            }

            if (serverIdToServerMap.ContainsKey(serverId))
            {
                throw new ArgumentException(String.Format("Server Id {0} already in collection.", serverId));
            }

            Server server = new Server(serverId);
            AddReplicasForServer(server);
            this.serverIdToServerMap[serverId] = server;
        }

        public void RemoveServer(int serverId)
        {
            if (!serverIdToServerMap.ContainsKey(serverId))
            {
                throw new ArgumentException(String.Format("Server Id {0} not found in collection.", serverId));
            }

            Server server = serverIdToServerMap[serverId];
            RemoveReplicasForServer(server);
            this.serverIdToServerMap.Remove(server.ServerId);
        }

		public int GetServerForClient(Guid clientId)
        {
			if (this.clientIdToClientRecordMap.ContainsKey(clientId))
			{
				return this.clientIdToClientRecordMap[clientId].ServerReplica.Server.ServerId;
			}

			int clientCircularIndex = GetCircularListIndexForGuid(clientId);
			int serverReplicaIndex = clientCircularIndex;
			while (circularList[serverReplicaIndex] == null)
			{
				serverReplicaIndex = GetNextCircularListIndex(serverReplicaIndex);
			}

			ServerReplica replica = circularList[serverReplicaIndex];
			replica.AddClient(clientId);
			this.clientIdToClientRecordMap[clientId] = new ClientRecord(clientCircularIndex, replica);

			return replica.Server.ServerId;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Consistent Hash");
            sb.AppendLine(string.Format("Max Num Servers: {0}", this.maxNumServers));
            sb.AppendLine(string.Format("Num Replicas Per Servers: {0}", this.numReplicasPerServer));
            sb.AppendLine(string.Format("Circular List Length: {0}", this.circularListLen));
             
            Dictionary<int, Server>.ValueCollection vc = this.serverIdToServerMap.Values;
            sb.AppendLine(string.Format("Num Servers In Collection: {0}", vc.Count));
            foreach (Server server in this.serverIdToServerMap.Values)
            {
                sb.Append(server.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }

        internal int GetNumServers()
        {
            return this.serverIdToServerMap.Count;
        }

        internal ConsistentHashSnapshot GetSnapshot()
        {
            Dictionary<Guid, int> clientIdToServerIdMap = new Dictionary<Guid, int>();
            foreach (Guid clientId in this.clientIdToClientRecordMap.Keys)
            {
                clientIdToServerIdMap[clientId] = this.clientIdToClientRecordMap[clientId].ServerReplica.Server.ServerId;
            }

            return new ConsistentHashSnapshot()
            {
                clientIdToServerIdMap = clientIdToServerIdMap
            };
        }

        internal int GetNumClients()
        {
            return this.clientIdToClientRecordMap.Count;
        }

        private void ReassignClientFromOriginalIndex(Guid clientId)
        {
            ClientRecord cr = this.clientIdToClientRecordMap[clientId];
            ServerReplica nextServerReplica = GetNextServerReplica(cr.CircularIndex);
            if (nextServerReplica.ReplicaId != cr.ServerReplica.ReplicaId)
            {
                cr.ServerReplica.RemoveClient(clientId);
                this.clientIdToClientRecordMap.Remove(clientId);

                nextServerReplica.AddClient(clientId);
                this.clientIdToClientRecordMap[clientId] = new ClientRecord(cr.CircularIndex, nextServerReplica); ;
            }
        }

        private void AddReplicasForServer(Server server)
        {
			List<ServerReplica> nextReplicas = new List<ServerReplica>();
            for (int i = 0; i < this.numReplicasPerServer; i++)
			{
				ServerReplica newReplica = CreateReplicaForServer(server);
				server.AddReplica(newReplica);
				this.circularList[newReplica.CircularListIndex] = newReplica;

				nextReplicas.Add(GetNextServerReplica(newReplica.CircularListIndex));
			}

			foreach (ServerReplica nextReplica in nextReplicas)
			{
				ReassignClients(nextReplica);
			}
		}

        private void RemoveReplicasForServer(Server server)
        {
            List<ServerReplica> removedReplicas = new List<ServerReplica>();
            foreach (ServerReplica removedReplica in server.GetReplicas())
            {
                removedReplicas.Add(removedReplica);
                this.circularList[removedReplica.CircularListIndex] = null;
            }

            foreach (ServerReplica removedReplica in removedReplicas)
            {
                ReassignClients(removedReplica);
            }
        }

        private void ReassignClients(ServerReplica serverReplica)
        {
            if (serverReplica.NumClients == 0)
            {
                return;
            }

            List<Guid> copyOfClients = new List<Guid>(serverReplica.GetCopyOfClients());
            foreach (Guid clientId in copyOfClients)
            {
                this.ReassignClientFromOriginalIndex(clientId);
            }
        }
        
        private ServerReplica GetNextServerReplica(int startIndex)
        {
            int index = GetNextCircularListIndex(startIndex);
            while (circularList[index] == null)
            {
                index = GetNextCircularListIndex(index);
            }

            return circularList[index];
        }

        private ServerReplica CreateReplicaForServer(Server server)
        {
            Guid replicaId = Guid.NewGuid();
            int circularListIndex = GetCircularListIndexForGuid(replicaId);
            while (this.circularList[circularListIndex] != null)
            {
                circularListIndex = GetNextCircularListIndex(circularListIndex);
            }

            return new ServerReplica(replicaId, circularListIndex, server);
        }

        private int GetCircularListIndexForGuid(Guid replicaId)
        {
            return Math.Abs(replicaId.GetHashCode()) % this.circularListLen;
        }

		private int GetNextCircularListIndex(int currentIndex)
        {
            return (currentIndex + 1) % this.circularListLen;
        }
    }
}
