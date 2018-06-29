using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Xml.Schema;

namespace Experiment
{
    internal class ConsistentHashSnapshot
    {
        public Dictionary<Guid, int> clientIdToServerIdMap;

        public string Compare(ConsistentHashSnapshot other)
        {
            StringBuilder sb = new StringBuilder();
            int thisClientCount = this.clientIdToServerIdMap.Count;
            sb.AppendLine(string.Format("This client count: {0}", thisClientCount));
            int otherClientCount = other.clientIdToServerIdMap.Count;
            sb.AppendLine(string.Format("Other client count: {0}", otherClientCount));

            int diffCount = 0;
            foreach (Guid clientId in this.clientIdToServerIdMap.Keys)
            {
                int thisServerId = this.clientIdToServerIdMap[clientId];
                int otherServerId = other.clientIdToServerIdMap[clientId];
                if (thisServerId != otherServerId)
                {
                    diffCount++;
                    sb.AppendLine(string.Format("ClientId {0}:  this.serverId: {1},  other.serverId: {2}",
                        clientId,
                        thisServerId,
                        otherServerId));
                }
            }

            if (diffCount == 0)
            {
                sb.AppendLine("No differences found in client mappings.");
            }
            else
            {
                sb.AppendLine(string.Format("{0} differences found in client mappings.", diffCount));
                sb.AppendLine(string.Format("Difference percentage: {0}", ((double)diffCount / thisClientCount) * 100.0));
            }

            return sb.ToString();
        }
    }
}
