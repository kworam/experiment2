using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Experiment
{
	public class KevinGraph : Graph
	{
		private readonly List<KevinGraphVertex> vertices = new List<KevinGraphVertex>();

		private readonly Dictionary<string, GraphVertex> uniqueVertexNameMap = new Dictionary<string, GraphVertex>();

		public KevinGraph()
		{
		}

		private KevinGraph(KevinGraph source)
		{
			this.vertices = new List<KevinGraphVertex>();
			foreach (GraphVertex v in source.vertices)
			{
				this.AddVertex(v.UniqueKey);
			}

			foreach (GraphVertex v in source.vertices)
			{
				foreach (GraphEdge edge in v.GetIncidentEdges())
				{
					this.AddEdge(edge.SourceVertexUniqueKey, edge.TargetVertexUniqueKey, edge.Weight);
				}
			}
		}

		public void AddVertex(string uniqueKey)
		{
			EnforceVertexUniqueKey(uniqueKey);

			InternalAddVertex(new KevinGraphVertex(this, uniqueKey));
		}

		public void AddEdge(string sourceUniqueKey, string targetUniqueKey, int weight)
		{
			KevinGraphVertex source = GetVertexByUniqueKey(sourceUniqueKey) as KevinGraphVertex;
			KevinGraphVertex target = GetVertexByUniqueKey(targetUniqueKey) as KevinGraphVertex;

			InternalAddEdge(source, target, weight);

			source.OutDegree++;
			target.InDegree++;
		}

		public void RemoveEdge(string sourceUniqueKey, string targetUniqueKey)
		{
			KevinGraphVertex source = GetVertexByUniqueKey(sourceUniqueKey) as KevinGraphVertex;
			KevinGraphVertex target = GetVertexByUniqueKey(targetUniqueKey) as KevinGraphVertex;

			InternalRemoveEdge(source, target);

			source.OutDegree--;
			target.InDegree--;
		}

		public int NumVertices
		{
			get
			{
				return vertices.Count;
			}
		}

		public int NumEdges
		{
			get
			{
				int numEdges = 0;
				vertices.ForEach(v => numEdges += v.GetIncidentEdges().Count());
				return numEdges;
			}
		}

		public bool ContainsVertex(string key)
		{
			return uniqueVertexNameMap.ContainsKey(key);
		}

		public GraphVertex GetVertexByUniqueKey(string key)
		{
			if (!ContainsVertex(key))
			{
				throw new KeyNotFoundException(string.Format("vertex '{0}' not found", key));
			}

			return uniqueVertexNameMap[key];
		}

		public IEnumerable<GraphVertex> GetAllVertices()
		{
			return this.vertices;
		}

		public object Clone()
		{
			return new KevinGraph(this);
		}


		public uint GetInDegreeForVertex(string vertexUniqueKey)
		{
			KevinGraphVertex kv = GetVertexByUniqueKey(vertexUniqueKey) as KevinGraphVertex;
			return kv.InDegree;
		}

		public uint GetOutDegreeForVertex(string vertexUniqueKey)
		{
			KevinGraphVertex kv = GetVertexByUniqueKey(vertexUniqueKey) as KevinGraphVertex;
			return kv.OutDegree;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (KevinGraphVertex v in this.vertices)
			{
				sb.Append(v.UniqueKey + " ");
				foreach (KevinGraphEdge e in v.GetIncidentEdges())
				{
					sb.Append(string.Format("{0}(w:{1}) ", e.TargetVertexUniqueKey, e.Weight));
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}

		public static Graph FromString(string s)
		{
			Dictionary<string, KevinGraphVertex> nameToVertexMap = new Dictionary<string, KevinGraphVertex>();

			KevinGraph g = new KevinGraph();
			if (string.IsNullOrEmpty(s))
			{
				return g;
			}

			using (StringReader sr = new StringReader(s))
			{
				while (true)
				{
					string currentLine = sr.ReadLine();
					if (currentLine == null)
					{
						break;
					}

					string[] vertices = currentLine.Split(new char[] { ' ' });
					if (vertices.Length == 0)
					{
						continue;
					}

					KevinGraphVertex vertex = CreateOrGetVertexByName(g, vertices[0], nameToVertexMap);
					if (!g.ContainsVertex(vertex.UniqueKey))
					{
						g.AddVertex(vertex.UniqueKey);
					}

					for (int i = 1; i < vertices.Length; i++)
					{
						GraphEdge edge = ParseEdge(g, vertex, vertices[i], nameToVertexMap);
						string targetVertexUniqueKey = edge.TargetVertexUniqueKey;
						if (!g.ContainsVertex(targetVertexUniqueKey))
						{
							g.AddVertex(targetVertexUniqueKey);
						}

						g.AddEdge(edge.SourceVertexUniqueKey, edge.TargetVertexUniqueKey, edge.Weight);
					}
				}

				return g;
			}
		}

		private static GraphEdge ParseEdge(
			Graph g,
			KevinGraphVertex source,
			string edgeString,
			Dictionary<string, KevinGraphVertex> nameToVertexMap)
		{
			string targetVertexName = null;
			int weight = 0;

			Regex attributesRegex = new Regex("\\(.*\\)");
			Match attributesMatch = attributesRegex.Match(edgeString);
			if (attributesMatch.Success)
			{
				targetVertexName = edgeString.Substring(0, attributesMatch.Index);

				string stripParens = attributesMatch.Value
					.Replace("(", string.Empty)
					.Replace(")", string.Empty);
				string[] keyValuePairs = stripParens.Split(new char[] { ':' });
				int i = 0;
				while (i < keyValuePairs.Length)
				{
					string attributeName = keyValuePairs[i++];
					string attributeValue = keyValuePairs[i++];
					switch (attributeName)
					{
						case "w":
							weight = int.Parse(attributeValue);
							break;
						default:
							throw new InvalidDataException(
								string.Format("Unrecognized edge attribute '{0}'", attributeName));
					}
				}
			}
			else
			{
				targetVertexName = edgeString;
			}

			KevinGraphVertex target = CreateOrGetVertexByName(g, targetVertexName, nameToVertexMap);
			return new KevinGraphEdge(g, source.UniqueKey, target.UniqueKey, weight);
		}

		private static KevinGraphVertex CreateOrGetVertexByName(
			Graph g,
			string vertexUniqueName,
			Dictionary<string, KevinGraphVertex> nameToVertexMap)
		{
			if (nameToVertexMap.ContainsKey(vertexUniqueName))
			{
				return nameToVertexMap[vertexUniqueName];
			}

			KevinGraphVertex v = new KevinGraphVertex(g, vertexUniqueName);
			nameToVertexMap[vertexUniqueName] = v;
			return v;
		}

		private void EnforceVertexUniqueKey(string vertexUniqueKey)
		{
			if (uniqueVertexNameMap.ContainsKey(vertexUniqueKey))
			{
				throw new GraphUniqueVertexNameViolationException(
					string.Format("Vertex vertexUniqueName '{0}' already in graph", vertexUniqueKey));
			}
		}

		private void AddToVertexUniqueKeyMap(GraphVertex v)
		{
			uniqueVertexNameMap[v.UniqueKey] = v;
		}

		private void InternalAddVertex(KevinGraphVertex v)
		{
			AddToVertexUniqueKeyMap(v);
			vertices.Add(v);
		}

		private void InternalAddEdge(KevinGraphVertex source, KevinGraphVertex target, int weight)
		{
			source.AddEdge(target.UniqueKey, weight);
		}

		public void InternalRemoveEdge(KevinGraphVertex source, KevinGraphVertex target)
		{
			source.RemoveEdge(target.UniqueKey);
		}
	}
}
