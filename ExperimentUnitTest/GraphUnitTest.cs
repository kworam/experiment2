using Experiment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExperimentUnitTest
{
	[TestClass]
	public class GraphUnitTest
	{
		[TestCategory("Graph"), TestMethod]
		public void EmptyGraph()
		{
			Graph g = GraphFactory.CreateGraph();
			Assert.AreEqual(g.NumVertices, 0);
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphBFT()
		{
			var g = GetTestUnweightedGraph();

			GraphBFT bft = new GraphBFT(g);
			StringVisitor visitor = new StringVisitor();
			bft.TraverseGraph(startingVertexUniqueKey: "A", visitor: visitor);
			Console.WriteLine(visitor);
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphDFT()
		{
			var g = GetTestUnweightedGraph();

			GraphDFT bft = new GraphDFT(g);
			StringVisitor visitor = new StringVisitor();
			bft.TraverseGraph(startingVertexUniqueKey: "A", visitor: visitor);
			Console.WriteLine(visitor);
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphShortestPath()
		{
			var g = GetTestWeightedGraph();

			GraphDijkstraShortestPath sp = new GraphDijkstraShortestPath(g);
			List<GraphVertex> shortestPath = sp.FindShortestPath(
				startVertexUniqueKey: "G",
				targetVertexUniqueKey: "F");
			Console.WriteLine(string.Join(" ", shortestPath.Select(v => v.UniqueKey)));
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphMinimumSpanningTree()
		{
			var g = GetTestWeightedGraph();

			GraphPrimMinimumSpanningTree mst = new GraphPrimMinimumSpanningTree(g);
			Graph mstGraph = mst.GetTree();
			Assert.AreEqual(mstGraph.NumVertices, g.NumVertices);
			Assert.AreEqual(mstGraph.NumEdges, (g.NumVertices * 2) - 2);
			Console.WriteLine(mstGraph);
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphClone()
		{
			Graph g = GetTestWeightedGraph();

			Graph clone = g.Clone() as Graph;

			Assert.AreEqual(g.NumVertices, clone.NumVertices);
			Assert.AreEqual(g.NumEdges, clone.NumEdges);
			Assert.AreNotEqual(g.GetVertexByUniqueKey("A"), clone.GetVertexByUniqueKey("A"));
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphInOutDegree()
		{
			Graph g = GetTestWeightedGraph();

			string aUniqueKey = "A";
			Assert.AreEqual(g.GetOutDegreeForVertex(aUniqueKey), (uint)2);
			Assert.AreEqual(g.GetInDegreeForVertex(aUniqueKey), (uint)2);
		}

		[TestCategory("Graph"), TestMethod]
		[ExpectedException(typeof(GraphEdgeNotFoundException))]
		public void GraphRemoveEdgeNotFound()
		{
			Graph g = GetTestWeightedGraph();

			GraphVertex a = g.GetVertexByUniqueKey("A");
			a.RemoveEdge("D");
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphRemoveEdge()
		{
			Graph g = GetTestWeightedGraph();

			string aUniqueKey = "A";
			Assert.AreEqual(g.GetOutDegreeForVertex(aUniqueKey), (uint)2);
			Assert.AreEqual(g.GetInDegreeForVertex(aUniqueKey), (uint)2);

			string bUniqueKey = "B";
			Assert.AreEqual(g.GetOutDegreeForVertex(bUniqueKey), (uint)3);
			Assert.AreEqual(g.GetInDegreeForVertex(bUniqueKey), (uint)3);

			g.RemoveEdge(aUniqueKey, bUniqueKey);

			Assert.AreEqual(g.GetOutDegreeForVertex(aUniqueKey), (uint)1);
			Assert.AreEqual(g.GetInDegreeForVertex(aUniqueKey), (uint)2);

			Assert.AreEqual(g.GetOutDegreeForVertex(bUniqueKey), (uint)3);
			Assert.AreEqual(g.GetInDegreeForVertex(bUniqueKey), (uint)2);

			g.RemoveEdge(bUniqueKey, aUniqueKey);

			Assert.AreEqual(g.GetOutDegreeForVertex(aUniqueKey), (uint)1);
			Assert.AreEqual(g.GetInDegreeForVertex(aUniqueKey), (uint)1);

			Assert.AreEqual(g.GetOutDegreeForVertex(bUniqueKey), (uint)2);
			Assert.AreEqual(g.GetInDegreeForVertex(bUniqueKey), (uint)2);
		}

		[TestCategory("Graph"), TestMethod]
		[ExpectedException(typeof(CyclicGraphException))]
		public void GraphTopSortCyclicGraph()
		{
			Graph g = GetCyclicDirectedGraph();

			IEnumerable<GraphTopologicalSortNode> topSort = GraphTopologicalSort.Sort(g);
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphTopSortSimpleDAG()
		{
			Graph g = GetSimpleDAG();

			IEnumerable<GraphTopologicalSortNode> topSort = GraphTopologicalSort.Sort(g);
			Assert.AreEqual("A:0 B:1 C:2", string.Join(" ", topSort));
		}

		[TestCategory("Graph"), TestMethod]
		public void GraphTopSortComplexDAG()
		{
			Graph g = GetComplexDAG();

			IEnumerable<GraphTopologicalSortNode> topSort = GraphTopologicalSort.Sort(g);
			Assert.AreEqual("A:0 B:0 G:0 C:1 D:1 E:2 F:3", string.Join(" ", topSort));
		}

		private static Graph GetCyclicDirectedGraph()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("A B");
			sb.AppendLine("B C");
			sb.AppendLine("C B");
			string graphString = sb.ToString();

			Graph g = KevinGraph.FromString(graphString);
			Assert.AreEqual(g.NumVertices, 3);
			return g;
		}
		private static Graph GetSimpleDAG()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("A B");
			sb.AppendLine("B C");
			string graphString = sb.ToString();

			Graph g = KevinGraph.FromString(graphString);
			Assert.AreEqual(g.NumVertices, 3);
			return g;
		}

		private static Graph GetComplexDAG()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("A C");
			sb.AppendLine("B C");
			sb.AppendLine("B D");
			sb.AppendLine("D E");
			sb.AppendLine("E F");
			sb.AppendLine("G E");
			string graphString = sb.ToString();

			Graph g = KevinGraph.FromString(graphString);
			Assert.AreEqual(g.NumVertices, 7);
			return g;
		}

		private static Graph GetTestUnweightedGraph()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("A B C");
			sb.AppendLine("B A D G");
			sb.AppendLine("C A E");
			sb.AppendLine("D B E F");
			sb.AppendLine("E C D F H");
			sb.AppendLine("F E D");
			sb.AppendLine("G B");
			sb.AppendLine("H E I J");
			sb.AppendLine("I H");
			sb.AppendLine("J H");
			string graphString = sb.ToString();

			Graph g = KevinGraph.FromString(graphString);
			Assert.AreEqual(g.NumVertices, 10);
			return g;
		}

		private static Graph GetTestWeightedGraph()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("A B(w:5) C(w:1)");
			//sb.AppendLine("B A(w:5) D(w:8) G(w:3)");  // shortest path:  GBDF
			sb.AppendLine("B A(w:5) D(w:12) G(w:3)");   // shortest path:  G B A C E F
			sb.AppendLine("C A(w:1) E(w:2)");
			//sb.AppendLine("D B(w:8) E(w:3) F(w:5)");  // shortest path:  GBDF
			sb.AppendLine("D B(w:12) E(w:3) F(w:5)");   // shortest path:  G B A C E F
			sb.AppendLine("E C(w:2) D(w:3) F(w:6) H(w:7)");
			sb.AppendLine("F E(w:6) D(w:5)");
			sb.AppendLine("G B(w:3)");
			sb.AppendLine("H E(w:7) I(w:3) J(w:8)");
			sb.AppendLine("I H(w:3)");
			sb.AppendLine("J H(w:8)");
			string graphString = sb.ToString();

			Graph g = KevinGraph.FromString(graphString);
			Assert.AreEqual(g.NumVertices, 10);
			return g;
		}

		private class StringVisitor : GraphVisitor
		{
			private StringBuilder sb = new StringBuilder();
			public void Visit(GraphVertex v)
			{
				sb.Append(string.Format("{0} ", v.UniqueKey));
			}

			public override string ToString()
			{
				return sb.ToString();
			}
		}
	}
}
