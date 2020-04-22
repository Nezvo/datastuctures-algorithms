using System;
using System.Collections.Generic;

namespace DataStructures
{
	public class BinaryTree
	{
		#region Internals and properties
		private Node root; 
		#endregion

		#region Public methods
		public void Add(int value)
		{
			var node = new Node(value);

			if (root == null)
			{
				root = node;
				return;
			}

			var current = root;
			while (true)
			{
				if (value < current.Value)
				{
					if (current.LeftChild == null)
					{
						current.LeftChild = node;
						break;
					}
					current = current.LeftChild;
				}
				else
				{
					if (current.RightChild == null)
					{
						current.RightChild = node;
						break;
					}
					current = current.RightChild;
				}
			}
		}

		public int? Min()
		{
			if (root == null)
				return null;

			return Min(root);
		}

		public int? Max()
		{
			if (root == null)
				return null;

			return Max(root);
		}

		public bool Contains(int value)
		{
			return Contains(root, value);
		}

		public bool Equals(BinaryTree other)
		{
			if (other == null)
				return false;

			return Equals(root, other.root);
		}

		public int Height() => Height(root);

		public int Size() => Size(root);

		public int CountLeaves() => CountLeaves(root);

		public void TraversePreOrder()
		{
			TraversePreOrder(root);
		}

		public void TraverseInOrder()
		{
			TraverseInOrder(root);
		}

		public void TraversePostOrder()
		{
			TraversePostOrder(root);
		}

		public List<int> GetNodesAtDistance(int distance)
		{
			var list = new List<int>();
			GetNodesAtDistance(root, distance, list);
			return list;
		}

		public void TraverseLevelOrder()
		{
			for (var i = 0; i <= Height(); i++)
			{
				foreach (var value in GetNodesAtDistance(i))
					Console.WriteLine(value);
			}
		}

		public bool AreSibling(int first, int second)
		{
			return AreSibling(root, first, second);
		}

		public List<int> GetAncestors(int value)
		{
			var list = new List<int>();
			GetAncestors(root, value, list);
			return list;
		}

		public bool IsBalanced() => IsBalanced(root);

		public bool IsPerfect()
		{
			return Size() == (Math.Pow(2, Height() + 1) - 1);
		}
		#endregion

		#region Private methods

		private void TraversePreOrder(Node node)
		{
			if (node == null)
				return;

			Console.WriteLine(node.Value);
			TraversePreOrder(node.LeftChild);
			TraversePreOrder(node.RightChild);
		}

		private void TraverseInOrder(Node node)
		{
			if (node == null)
				return;

			TraverseInOrder(node.LeftChild);
			Console.WriteLine(node.Value);
			TraverseInOrder(node.RightChild);
		}

		private void TraversePostOrder(Node node)
		{
			if (node == null)
				return;

			TraversePostOrder(node.LeftChild);
			TraversePostOrder(node.RightChild);
			Console.WriteLine(node.Value);
		}

		private int Height(Node node)
		{
			if (node == null)
				return -1;

			if (IsLeaf(node))
				return 0;

			return 1 + Math.Max(
							Height(node.LeftChild),
							Height(node.RightChild));
		}

		private bool IsLeaf(Node node)
		{
			return node.LeftChild == null && node.RightChild == null;
		}

		private int Min(Node node)
		{
			if (node.LeftChild == null)
				return node.Value;

			return Min(node.LeftChild);
		}

		private int Max(Node node)
		{
			if (node.RightChild == null)
				return node.Value;

			return Max(node.RightChild);
		}

		private bool Equals(Node first, Node second)
		{
			if (first == null && second == null)
				return true;

			if (first != null && second != null)
				return first.Value == second.Value
								&& Equals(first.LeftChild, second.LeftChild)
								&& Equals(first.RightChild, second.RightChild);

			return false;
		}

		private void GetNodesAtDistance(Node node, int distance, List<int> list)
		{
			if (node == null)
				return;

			if (distance == 0)
			{
				list.Add(node.Value);
				return;
			}

			GetNodesAtDistance(node.LeftChild, distance - 1, list);
			GetNodesAtDistance(node.RightChild, distance - 1, list);
		}

		private int Size(Node node)
		{
			if (node == null)
				return 0;

			if (IsLeaf(node))
				return 1;

			return 1 + Size(node.LeftChild) + Size(node.RightChild);
		}

		private int CountLeaves(Node node)
		{
			if (node == null)
				return 0;

			if (IsLeaf(node))
				return 1;

			return CountLeaves(node.LeftChild) + CountLeaves(node.RightChild);
		}

		private bool Contains(Node node, int value)
		{
			if (node == null)
				return false;

			if (node.Value == value)
				return true;

			return Contains(node.LeftChild, value) || Contains(node.RightChild, value);
		}

		private bool AreSibling(Node node, int first, int second)
		{
			if (node == null)
				return false;

			var result = false;
			if (node.LeftChild != null && node.RightChild != null)
			{
				result = (node.LeftChild.Value == first && node.RightChild.Value == second) ||
										 (node.RightChild.Value == first && node.LeftChild.Value == second);
			}

			return result ||
							AreSibling(node.LeftChild, first, second) ||
							AreSibling(node.RightChild, first, second);
		}

		private bool GetAncestors(Node node, int value, List<int> list)
		{
			// We should traverse the tree until we find the target value. If
			// find the target value, we return true without adding the current node
			// to the list; otherwise, if we ask for ancestors of 5, 5 will be also
			// added to the list.
			if (node == null)
				return false;

			if (node.Value == value)
				return true;

			// If we find the target value in the left or right sub-trees, that means
			// the current node (root) is one of the ancestors. So we add it to the list.
			if (GetAncestors(node.LeftChild, value, list) ||
					GetAncestors(node.RightChild, value, list))
			{
				list.Add(node.Value);
				return true;
			}

			return false;
		}

		private bool IsBalanced(Node node)
		{
			if (node == null)
				return true;

			var balanceFactor = Height(node.LeftChild) - Height(node.RightChild);

			return Math.Abs(balanceFactor) <= 1 &&
							IsBalanced(node.LeftChild) &&
							IsBalanced(node.RightChild);
		}
		#endregion

		#region Helper classes
		private class Node
		{
			public int Value { get; set; }
			public Node LeftChild { get; set; }
			public Node RightChild { get; set; }

			public Node(int value)
			{
				Value = value;
			}
		}
		#endregion
	}
}
