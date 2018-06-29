using System;
using System.Linq;
using System.Collections.Generic;
using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExperimentUnitTest
{
    [TestClass]
    public class ConsistentHashUnitTest
    {
        [TestCategory("ConsistentHash"), TestMethod]
        public void EmptyConsistentHashToString()
        {
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 10);
            Console.WriteLine(ch.ToString());
			Assert.AreEqual(ch.GetNumServers(), 0);
			Assert.AreEqual(ch.GetNumClients(), 0);
        }

		[TestCategory("ConsistentHash"), TestMethod]
		public void SingleServerConsistentHashToString()
        {
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 10);
            ch.AddServer(0);
            Console.WriteLine(ch.ToString());
			Assert.AreEqual(ch.GetNumServers(), 1);
			Assert.AreEqual(ch.GetNumClients(), 0);
		}

		[TestCategory("ConsistentHash"), TestMethod]
		public void DoubleServerConsistentHashToString()
        {
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 10);
            ch.AddServer(0);
            ch.AddServer(1);
            Console.WriteLine(ch.ToString());
			Assert.AreEqual(ch.GetNumServers(), 2);
			Assert.AreEqual(ch.GetNumClients(), 0);
		}

		[TestCategory("ConsistentHash"), TestMethod]
		public void SingleServerGetServerForClient()
		{
			KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 10);
			int serverId = 0;
			Guid clientId = Guid.NewGuid();
			ch.AddServer(serverId);
			int assignedServerId = ch.GetServerForClient(clientId);
			Assert.AreEqual(ch.GetNumServers(), 1);
			Assert.AreEqual(ch.GetNumClients(), 1);
			Assert.AreEqual(serverId, assignedServerId);
		}

		[TestCategory("ConsistentHash"), TestMethod]
		public void FiveServersGetServerForClient()
		{
			KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 2);
			List<int> serverIds = new List<int>() { 0, 1, 2, 3, 4 };
			serverIds.ForEach(serverId => ch.AddServer(serverId));
			Guid clientId = Guid.NewGuid();
			int assignedServerId = ch.GetServerForClient(clientId);
			Assert.AreEqual(ch.GetNumServers(), serverIds.Count);
			Assert.AreEqual(ch.GetNumClients(), 1);
			Console.WriteLine(
				string.Format("ClientId: {0}  Assigned Server Id: {1}", clientId, assignedServerId));
			Console.WriteLine(ch.ToString());
		}

		[TestCategory("ConsistentHash"), TestMethod]
		public void FiveServersGetServerForManyClients()
		{
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers: 100, numReplicasPerServer: 2);
			List<int> serverIds = new List<int>() { 0, 1, 2, 3, 4 };
			serverIds.ForEach(serverId => ch.AddServer(serverId));
			int numClients = 100;
			for (int i = 0; i < numClients; i++)
			{
				ch.GetServerForClient(Guid.NewGuid());
			}
			Assert.AreEqual(ch.GetNumServers(), serverIds.Count);
			Assert.AreEqual(ch.GetNumClients(), numClients);
			Console.WriteLine(ch.ToString());
		}

		[TestCategory("ConsistentHash"), TestMethod]
		public void TenServersTenReplicasThousandClientsAddServer()
		{
		    int maxNumServers = 100;
		    int numReplicasPerServer = 10;
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers, numReplicasPerServer);
            
            int numServers = 10;
            Enumerable.Range(0, numServers).ToList().ForEach(serverId => ch.AddServer(serverId));

			int numClients = 1000;
			for (int i = 0; i < numClients; i++)
			{
				ch.GetServerForClient(Guid.NewGuid());
			}
			Assert.AreEqual(ch.GetNumServers(), numServers);
			Assert.AreEqual(ch.GetNumClients(), numClients);
		    ConsistentHashSnapshot beforeSnapshot = ch.GetSnapshot();

			ch.AddServer(numServers);

            Assert.AreEqual(ch.GetNumServers(), numServers + 1);
			Assert.AreEqual(ch.GetNumClients(), numClients);
            ConsistentHashSnapshot afterSnapshot = ch.GetSnapshot();

            Console.WriteLine(beforeSnapshot.Compare(afterSnapshot));
        }

        [TestCategory("ConsistentHash"), TestMethod]
        public void TenServersTenReplicasThousandClientsRemoveServer()
        {
            int maxNumServers = 100;
            int numReplicasPerServer = 10;
            KevinConsistentHash ch = new KevinConsistentHash(maxNumServers, numReplicasPerServer);

            int numServers = 10;
            Enumerable.Range(0, numServers).ToList().ForEach(serverId => ch.AddServer(serverId));

            int numClients = 1000;
            for (int i = 0; i < numClients; i++)
            {
                ch.GetServerForClient(Guid.NewGuid());
            }
            Assert.AreEqual(ch.GetNumServers(), numServers);
            Assert.AreEqual(ch.GetNumClients(), numClients);
            ConsistentHashSnapshot beforeSnapshot = ch.GetSnapshot();

            ch.RemoveServer(numServers - 1);

            Assert.AreEqual(ch.GetNumServers(), numServers - 1);
            Assert.AreEqual(ch.GetNumClients(), numClients);
            ConsistentHashSnapshot afterSnapshot = ch.GetSnapshot();

            Console.WriteLine(beforeSnapshot.Compare(afterSnapshot));
        }
    }
}
