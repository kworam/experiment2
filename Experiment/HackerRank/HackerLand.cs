using System.Collections.Generic;
using System.Linq;

namespace Experiment.HackerRank
{
	class HackerLand
	{
		// Complete the roadsAndLibraries function below.
		public static long roadsAndLibraries(int n, int c_lib, int c_road, int[][] cities)
		{
			if (c_lib <= c_road)
			{
				return (long)n * c_lib;
			}

			Graph g = new Graph(n);

			for (int cityId = 1; cityId <= n; cityId++)
			{
				g.AddNode(cityId);
			}

			HashSet<int> setCitiesWithRoads = new HashSet<int>();
			HashSet<int> setCitiesWithNoRoads = g.GetSetAllNodes();
			for (int i = 0; i < cities.Length; i++)
			{
				g.AddEdge(cities[i][0], cities[i][1]);

				setCitiesWithNoRoads.Remove(cities[i][0]);
				setCitiesWithNoRoads.Remove(cities[i][1]);

				setCitiesWithRoads.Add(cities[i][0]);
				setCitiesWithRoads.Add(cities[i][1]);
			}

			long result = setCitiesWithNoRoads.Count * (long) c_lib;

			long totalRoads = 0;
			long numConnectedComponents = 0;
			while (setCitiesWithRoads.Count > 0)
			{
				int startCity = setCitiesWithRoads.First();
				totalRoads += TraverseConnectedComponent(g, startCity, setCitiesWithRoads);
				numConnectedComponents++;
			}

			return result + (totalRoads * c_road) + (numConnectedComponents * c_lib);
		}

		private static long TraverseConnectedComponent(
			Graph g, 
			int startCity, 
			HashSet<int> citiesToFind
			)
		{
			HashSet<int> foundCities = new HashSet<int>();

			Queue<int> citiesToExplore = new Queue<int>();
			citiesToExplore.Enqueue(startCity);
			foundCities.Add(startCity);
			citiesToFind.Remove(startCity);

			long numCitiesInConnectedComponent = 0;
			while (citiesToExplore.Count > 0)
			{
				int currentCity = citiesToExplore.Dequeue();
				numCitiesInConnectedComponent++;

				Node currentNode = g.GetNode(currentCity);
				foreach (int adjId in currentNode.GetAdjacentNodes())
				{
					if (!foundCities.Contains(adjId))
					{
						foundCities.Add(adjId);
						citiesToExplore.Enqueue(adjId);
						citiesToFind.Remove(adjId);
					}
				}
			}

			return numCitiesInConnectedComponent - 1;
		}

		private class Graph
		{
			private readonly Node[] nodes;

			public Graph(int numNodes)
			{
				nodes = new Node[numNodes];
			}

			public HashSet<int> GetSetAllNodes()
			{
				HashSet<int> set = new HashSet<int>();
				for (int i = 1; i <= nodes.Length; i++)
				{
					set.Add(i);
				}
				return set;
			}

			public Node GetNode(int cityId)
			{
				return nodes[cityId-1];
			}

			public void AddNode(int cityId)
			{
				if (nodes[cityId-1] == null)
				{
					nodes[cityId - 1] = new Node();
				}
			}

			public void AddEdge(int sourceCityId, int targetCityId)
			{
				AddNode(sourceCityId);
				AddNode(targetCityId);

				GetNode(sourceCityId).AddAdjacentNode(targetCityId);
				GetNode(targetCityId).AddAdjacentNode(sourceCityId);
			}
		}

		private class Node
		{
			private readonly List<int> adjacentNodes = new List<int>();

			public void AddAdjacentNode(int aCityId)
			{
				if (!adjacentNodes.Contains(aCityId))
				{
					adjacentNodes.Add(aCityId);
				}
			}

			public IEnumerable<int> GetAdjacentNodes()
			{
				return adjacentNodes;
			}
		}
	}
}
