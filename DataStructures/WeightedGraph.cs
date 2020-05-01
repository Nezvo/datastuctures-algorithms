using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Helpers;

namespace DataStructures
{
	public class WeightedGraph
	{
		#region Internals and properties
		private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();
		#endregion

		#region Public methods
		public void AddNode(string label)
		{
			if (!nodes.ContainsKey(label))
				nodes.Add(label, new Node(label));
		}

		public void AddEdge(string from, string to, int weight)
		{
			var fromNode = nodes[from];
			if (fromNode == null)
				throw new ArgumentException();

			var toNode = nodes[to];
			if (toNode == null)
				throw new ArgumentException();

			fromNode.AddEdge(toNode, weight);
			toNode.AddEdge(fromNode, weight);
		}

		public void Print()
		{
			foreach (var node in nodes.Values)
			{
				var edges = node.Edges;
				if (edges.Any())
					Console.WriteLine($"{node.Label} is connected to {string.Join(", ", edges.Select(x => x.To.Label))}");
			}
		}

		public List<string> GetShortestPath(string from, string to)
		{
			var fromNode = nodes[from];
			if (fromNode == null)
				throw new ArgumentException();

			var toNode = nodes[to];
			if (toNode == null)
				throw new ArgumentException();

			var distances = new Dictionary<Node, int>();
			var previousNodes = new Dictionary<Node, Node>();
			foreach (var node in nodes.Values)
				distances.Add(node, int.MaxValue);
			distances[fromNode] = 0;
			previousNodes[fromNode] = null;

			var visited = new HashSet<Node>();

			PriorityQueueWithArray<NodeEntry, int> queue = new PriorityQueueWithArray<NodeEntry, int>(nodes.Count, (nodeEntry) => nodeEntry.Priority, PriorityQueueType.Min);
			queue.Enqueue(new NodeEntry(fromNode, 0));

			while (!queue.IsEmpty())
			{
				var current = queue.Dequeue().Node;
				visited.Add(current);

				foreach (var edge in current.Edges)
				{
					if (visited.Contains(edge.To))
						continue;

					var newDistance = distances[current] + edge.Weight;
					if (newDistance < distances[edge.To])
					{
						distances[edge.To] = newDistance;
						previousNodes[edge.To] = current;
						queue.Enqueue(new NodeEntry(edge.To, newDistance));
					}
				}
			}

			return BuildPath(previousNodes, toNode);
		}

		public bool HasCycle()
		{
			var visited = new HashSet<Node>();

			foreach (var node in nodes.Values)
			{
				if (!visited.Contains(node) &&
						HasCycle(node, null, visited))
					return true;
			}

			return false;
		}

		public WeightedGraph GetMinimumSpanningTree()
		{
			var tree = new WeightedGraph();

			if (!nodes.Any())
				return tree;

			var elementCount = nodes.Values.SelectMany(x => x.Edges).Count();
			PriorityQueueWithArray<Edge, int> edges = new PriorityQueueWithArray<Edge, int>(elementCount, (edge) => edge.Weight, PriorityQueueType.Min);

			var enumerator = nodes.Values.GetEnumerator();
			enumerator.MoveNext();
			var startNode = enumerator.Current;
			foreach (var edge in startNode.Edges)
				edges.Enqueue(edge);
			tree.AddNode(startNode.Label);

			if (edges.IsEmpty())
				return tree;

			while (tree.nodes.Count < nodes.Count)
			{
				var minEdge = edges.Dequeue();
				var nextNode = minEdge.To;

				if (tree.ContainsNode(nextNode.Label))
					continue;

				tree.AddNode(nextNode.Label);
				tree.AddEdge(minEdge.From.Label,
								nextNode.Label, minEdge.Weight);

				foreach (var edge in nextNode.Edges)
					if (!tree.ContainsNode(edge.To.Label))
						edges.Enqueue(edge);
			}

			return tree;
		}

		public bool ContainsNode(string label) => nodes.ContainsKey(label);
		#endregion

		#region Private methods
		private List<string> BuildPath(Dictionary<Node, Node> previousNodes, Node toNode)
		{
			var stack = new Stack(nodes.Count);
			stack.Push(toNode);
			var previous = previousNodes[toNode];
			while (previous != null)
			{
				stack.Push(previous);
				previous = previousNodes[previous];
			}

			var path = new List<string>();
			while (stack.Count != 0)
				path.Add(((Node)stack.Pop()).Label);

			return path;
		}

		private bool HasCycle(Node node, Node parent, HashSet<Node> visited)
		{
			visited.Add(node);

			foreach (var edge in node.Edges)
			{
				if (edge.To == parent)
					continue;

				if (visited.Contains(edge.To) ||
						HasCycle(edge.To, node, visited))
					return true;
			}

			return false;
		}
		#endregion

		#region Helper classes
		private class Node
		{
			public string Label { get; set; }
			public List<Edge> Edges { get; set; }

			public Node(string label)
			{
				Label = label;
				Edges = new List<Edge>();
			}

			public void AddEdge(Node to, int weight) => Edges.Add(new Edge(this, to, weight));
		}

		private class Edge
		{
			public Node From { get; set; }
			public Node To { get; set; }
			public int Weight { get; set; }

			public Edge(Node from, Node to, int weight)
			{
				From = from;
				To = to;
				Weight = weight;
			}
		}

		private class NodeEntry
		{
			public Node Node { get; set; }
			public int Priority { get; set; }

			public NodeEntry(Node node, int priority)
			{
				Node = node;
				Priority = priority;
			}
		}
		#endregion
	}
}
