using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
	public class Graph
	{
		#region Internals and properties
		private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();
		private readonly Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();
		#endregion

		#region Public methods
		public void AddNode(string label)
		{
			var node = new Node(label);
			if (!nodes.ContainsKey(label))
				nodes.Add(label, node);
			if (!adjacencyList.ContainsKey(node))
				adjacencyList.Add(node, new List<Node>());
		}

		public void AddEdge(string from, string to)
		{
			var fromNode = nodes[from];
			if (fromNode == null)
				throw new ArgumentException();

			var toNode = nodes[to];
			if (toNode == null)
				throw new ArgumentException();

			adjacencyList[fromNode].Add(toNode);
		}

		public void Print()
		{
			foreach (var source in adjacencyList.Keys)
			{
				var targets = adjacencyList[source];
				if (targets.Any())
					Console.WriteLine($"{source.Label} is connected to {string.Join(", ", targets.Select(x => x.Label))}");
			}
		}

		public void RemoveNode(string label)
		{
			var node = nodes[label];
			if (node == null)
				return;

			foreach (var item in adjacencyList.Values)
				item.Remove(node);

			adjacencyList.Remove(node);
			nodes.Remove(label);
		}

		public void RemoveEdge(string from, string to)
		{
			var fromNode = nodes[from];
			var toNode = nodes[to];

			if (fromNode == null || toNode == null)
				return;

			adjacencyList[fromNode].Remove(toNode);
		}

		public void TraverseDepthFirst(string root)
		{
			var node = nodes[root];
			if (node == null)
				return;

			TraverseDepthFirst(node, new HashSet<Node>());
		}

		public void TraverseBreadthFirst(string root)
		{
			var node = nodes[root];
			if (node == null)
				return;

			var visited = new HashSet<Node>();

			var queue = new Queue<Node>();
			queue.Enqueue(node);

			while (queue.Any())
			{
				var current = queue.Dequeue();

				if (visited.Contains(current))
					continue;

				Console.WriteLine(current.Label);
				visited.Add(current);

				foreach (var neighbour in adjacencyList[current])
					if (!visited.Contains(neighbour))
						queue.Enqueue(neighbour);
			}
		}

		public List<string> TopologicalSort()
		{
			var stack = new Stack();
			var visited = new HashSet<Node>();

			foreach (var node in nodes.Values)
				TopologicalSort(node, visited, stack);

			var sorted = new List<string>();
			while (stack.Count != 0)
				sorted.Add(((Node)stack.Pop()).Label);

			return sorted;
		}

		public bool HasCycle()
		{
			var all = new HashSet<Node>();
			foreach (var node in nodes.Values)
				all.Add(node);

			var visiting = new HashSet<Node>();
			var visited = new HashSet<Node>();

			while (all.Any())
			{
				var enumerator = all.GetEnumerator();
				enumerator.MoveNext();
				if (HasCycle(enumerator.Current, all, visiting, visited))
					return true;
			}

			return false;
		}
		#endregion

		#region Private methods
		private void TraverseDepthFirst(Node root, HashSet<Node> visited)
		{
			Console.WriteLine(root.Label);
			visited.Add(root);

			foreach (var node in adjacencyList[root])
				if (!visited.Contains(node))
					TraverseDepthFirst(node, visited);
		}

		private void TopologicalSort(Node node, HashSet<Node> visited, Stack stack)
		{
			if (visited.Contains(node))
				return;

			visited.Add(node);

			foreach (var neighbour in adjacencyList[node])
				TopologicalSort(neighbour, visited, stack);

			stack.Push(node);
		}

		private bool HasCycle(Node node, HashSet<Node> all, HashSet<Node> visiting, HashSet<Node> visited)
		{
			all.Remove(node);
			visiting.Add(node);

			foreach (var neighbour in adjacencyList[node])
			{
				if (visited.Contains(neighbour))
					continue;

				if (visiting.Contains(neighbour))
					return true;

				if (HasCycle(neighbour, all, visiting, visited))
					return true;
			}

			visiting.Remove(node);
			visited.Add(node);

			return false;
		}
		#endregion

		#region Helper classes
		private class Node
		{
			public string Label { get; set; }

			public Node(string label)
			{
				Label = label;
			}
		}
		#endregion
	}
}
