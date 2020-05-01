using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
	public class Graph<T> where T : IComparable
	{
		#region Internals and properties
		private readonly Dictionary<string, INode<T>> nodes = new Dictionary<string, INode<T>>();
		private readonly Dictionary<INode<T>, List<INode<T>>> adjacencyList = new Dictionary<INode<T>, List<INode<T>>>();
		#endregion

		#region Public methods
		public void AddNode(INode<T> node)
		{
			if (!nodes.ContainsKey(node.Label))
				nodes.Add(node.Label, node);
			if (!adjacencyList.ContainsKey(node))
				adjacencyList.Add(node, new List<INode<T>>());
		}

		public void AddEdge(INode<T> fromNode, INode<T> toNode)
		{
			if (fromNode == null)
				throw new ArgumentException();

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

			TraverseDepthFirst(node, new HashSet<INode<T>>());
		}

		public void TraverseBreadthFirst(string root)
		{
			var node = nodes[root];
			if (node == null)
				return;

			var visited = new HashSet<INode<T>>();

			var queue = new Queue<INode<T>>();
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
			var stack = new Stack(nodes.Count);
			var visited = new HashSet<INode<T>>();

			foreach (var node in nodes.Values)
				TopologicalSort(node, visited, stack);

			var sorted = new List<string>();
			while (stack.Count != 0)
				sorted.Add(((INode<T>)stack.Pop()).Label);

			return sorted;
		}

		public bool HasCycle()
		{
			var all = new HashSet<INode<T>>();
			foreach (var node in nodes.Values)
				all.Add(node);

			var visiting = new HashSet<INode<T>>();
			var visited = new HashSet<INode<T>>();

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
		private void TraverseDepthFirst(INode<T> root, HashSet<INode<T>> visited)
		{
			Console.WriteLine(root.Label);
			visited.Add(root);

			foreach (var node in adjacencyList[root])
				if (!visited.Contains(node))
					TraverseDepthFirst(node, visited);
		}

		private void TopologicalSort(INode<T> node, HashSet<INode<T>> visited, Stack stack)
		{
			if (visited.Contains(node))
				return;

			visited.Add(node);

			foreach (var neighbour in adjacencyList[node])
				TopologicalSort(neighbour, visited, stack);

			stack.Push(node);
		}

		private bool HasCycle(INode<T> node, HashSet<INode<T>> all, HashSet<INode<T>> visiting, HashSet<INode<T>> visited)
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
	}
}
