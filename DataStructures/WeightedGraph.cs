using DataStructures.Helpers;
using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
	public class WeightedGraph<T> where T : IComparable
	{
		#region Internals and properties
		private readonly Dictionary<T, Node> nodes = new Dictionary<T, Node>();
		#endregion

		#region Public methods
		public void AddNode(INode<T> node)
		{
			if (!nodes.ContainsKey(node.Id))
				nodes.Add(node.Id, new Node(node));
		}

		public void AddEdge(INode<T> from, INode<T> to, double weight)
		{
			var fromNode = nodes[from.Id];
			if (fromNode == null)
				throw new ArgumentException();

			var toNode = nodes[to.Id];
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
					Console.WriteLine($"{node.Name} is connected to {string.Join(", ", edges.Select(x => x.To.Name))}");
			}
		}

		public List<string> GetShortestPath(INode<T> from, INode<T> to)
		{
			var fromNode = nodes[from.Id];
			if (fromNode == null)
				throw new ArgumentException();

			var toNode = nodes[to.Id];
			if (toNode == null)
				throw new ArgumentException();

			var distances = new Dictionary<Node, double>();
			var previousNodes = new Dictionary<Node, Node>();
			foreach (var node in nodes.Values)
				distances.Add(node, int.MaxValue);
			distances[fromNode] = 0;
			previousNodes[fromNode] = null;

			var visited = new HashSet<Node>();

			PriorityQueueWithArray queue = new PriorityQueueWithArray(nodes.Count, PriorityQueueType.Min);
			queue.Enqueue(new NodeEntry(fromNode, 0));

			while (!queue.IsEmpty())
			{
				var current = ((NodeEntry)queue.Dequeue()).Node;
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

		public WeightedGraph<T> GetMinimumSpanningTree()
		{
			var tree = new WeightedGraph<T>();

			if (!nodes.Any())
				return tree;

			var elementCount = nodes.Values.SelectMany(x => x.Edges).Count();
			PriorityQueueWithArray edges = new PriorityQueueWithArray(elementCount, PriorityQueueType.Min);

			var enumerator = nodes.Values.GetEnumerator();
			enumerator.MoveNext();
			var startNode = enumerator.Current;
			foreach (var edge in startNode.Edges)
				edges.Enqueue(edge);
			tree.AddNode(startNode);

			if (edges.IsEmpty())
				return tree;

			while (tree.nodes.Count < nodes.Count)
			{
				var minEdge = (Edge)edges.Dequeue();
				var nextNode = minEdge.To;

				if (tree.ContainsId(nextNode.Id))
					continue;

				tree.AddNode(nextNode);
				tree.AddEdge(minEdge.From, nextNode, minEdge.Weight);

				foreach (var edge in nextNode.Edges)
					if (!tree.ContainsId(edge.To.Id))
						edges.Enqueue(edge);
			}

			return tree;
		}

		public bool ContainsId(T id) => nodes.ContainsKey(id);
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
				path.Add(((Node)stack.Pop()).Name);

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
		private class Node : INode<T>
		{
			public T Id { get; set; }
			public string Name { get; set; }
			public List<Edge> Edges { get; set; }

			public Node(INode<T> node)
			{
				Id = node.Id;
				Name = node.Name;
				Edges = new List<Edge>();
			}

			public void AddEdge(Node to, double weight) => Edges.Add(new Edge(this, to, weight));
		}

		private class Edge : IPriorityQueueItem
		{
			public Node From { get; set; }
			public Node To { get; set; }
			public double Weight { get; set; }
			public double Priority { get; set; }

			public Edge(Node from, Node to, double weight)
			{
				From = from;
				To = to;
				Weight = weight;
				Priority = weight;
			}
		}

		private class NodeEntry : IPriorityQueueItem
		{
			public Node Node { get; set; }
			public double Priority { get; set; }

			public NodeEntry(Node node, double priority)
			{
				Node = node;
				Priority = priority;
			}
		}
		#endregion
	}
}
