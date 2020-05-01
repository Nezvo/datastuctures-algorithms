using DataStructures.Interfaces;
using System;
using System.Collections.Generic;

namespace DataStructures
{
	public class BinaryTree<T> where T : IComparable
	{
		#region Internals and properties
		private IBinaryNode<T> root; 
		#endregion

		#region Public methods
		public void Add(IBinaryNode<T> node)
		{
			if (root == null)
			{
				root = node;
				return;
			}

			var current = root;
			while (true)
			{
				if (node.Value.CompareTo(current.Value) < 0)
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

		public T Min()
		{
			if (root == null)
				throw new InvalidOperationException();

			return Min(root);
		}

		public T Max()
		{
			if (root == null)
				throw new InvalidOperationException();

			return Max(root);
		}

		public bool Contains(T value) => Contains(root, value);

		public bool Equals(BinaryTree<T> other)
		{
			if (other == null)
				return false;

			return Equals(root, other.root);
		}

		public int Height() => Height(root);

		public int Size() => Size(root);

		public int CountLeaves() => CountLeaves(root);

		public void TraversePreOrder() => TraversePreOrder(root);

		public void TraverseInOrder() => TraverseInOrder(root);

		public void TraversePostOrder() => TraversePostOrder(root);

		public List<T> GetNodesAtDistance(int distance)
		{
			var list = new List<T>();
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

		public bool AreSibling(T first, T second) => AreSibling(root, first, second);

		public List<T> GetAncestors(T value)
		{
			var list = new List<T>();
			GetAncestors(root, value, list);
			return list;
		}

		public bool IsBalanced() => IsBalanced(root);

		public bool IsPerfect() => Size() == (Math.Pow(2, Height() + 1) - 1);
		#endregion

		#region Private methods

		private void TraversePreOrder(IBinaryNode<T> node)
		{
			if (node == null)
				return;

			Console.WriteLine(node.Value);
			TraversePreOrder(node.LeftChild);
			TraversePreOrder(node.RightChild);
		}

		private void TraverseInOrder(IBinaryNode<T> node)
		{
			if (node == null)
				return;

			TraverseInOrder(node.LeftChild);
			Console.WriteLine(node.Value);
			TraverseInOrder(node.RightChild);
		}

		private void TraversePostOrder(IBinaryNode<T> node)
		{
			if (node == null)
				return;

			TraversePostOrder(node.LeftChild);
			TraversePostOrder(node.RightChild);
			Console.WriteLine(node.Value);
		}

		private int Height(IBinaryNode<T> node)
		{
			if (node == null)
				return -1;

			if (IsLeaf(node))
				return 0;

			return 1 + Math.Max(
							Height(node.LeftChild),
							Height(node.RightChild));
		}

		private bool IsLeaf(IBinaryNode<T> node) => node.LeftChild == null && node.RightChild == null;

		private T Min(IBinaryNode<T> node)
		{
			if (node.LeftChild == null)
				return node.Value;

			return Min(node.LeftChild);
		}

		private T Max(IBinaryNode<T> node)
		{
			if (node.RightChild == null)
				return node.Value;

			return Max(node.RightChild);
		}

		private bool Equals(IBinaryNode<T> first, IBinaryNode<T> second)
		{
			if (first == null && second == null)
				return true;

			if (first != null && second != null)
				return first.Value.CompareTo(second.Value) == 0
								&& Equals(first.LeftChild, second.LeftChild)
								&& Equals(first.RightChild, second.RightChild);

			return false;
		}

		private void GetNodesAtDistance(IBinaryNode<T> node, int distance, List<T> list)
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

		private int Size(IBinaryNode<T> node)
		{
			if (node == null)
				return 0;

			if (IsLeaf(node))
				return 1;

			return 1 + Size(node.LeftChild) + Size(node.RightChild);
		}

		private int CountLeaves(IBinaryNode<T> node)
		{
			if (node == null)
				return 0;

			if (IsLeaf(node))
				return 1;

			return CountLeaves(node.LeftChild) + CountLeaves(node.RightChild);
		}

		private bool Contains(IBinaryNode<T> node, T value)
		{
			if (node == null)
				return false;

			if (node.Value.CompareTo(value) == 0)
				return true;

			return Contains(node.LeftChild, value) || Contains(node.RightChild, value);
		}

		private bool AreSibling(IBinaryNode<T> node, T first, T second)
		{
			if (node == null)
				return false;

			var result = false;
			if (node.LeftChild != null && node.RightChild != null)
			{
				result = (node.LeftChild.Value.CompareTo(first) == 0 && node.RightChild.Value.CompareTo(second) == 0) ||
										 (node.RightChild.Value.CompareTo(first) == 0 && node.LeftChild.Value.CompareTo(second) == 0);
			}

			return result ||
							AreSibling(node.LeftChild, first, second) ||
							AreSibling(node.RightChild, first, second);
		}

		private bool GetAncestors(IBinaryNode<T> node, T value, List<T> list)
		{
			// We should traverse the tree until we find the target value. If
			// find the target value, we return true without adding the current node
			// to the list; otherwise, if we ask for ancestors of 5, 5 will be also
			// added to the list.
			if (node == null)
				return false;

			if (node.Value.CompareTo(value) == 0)
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

		private bool IsBalanced(IBinaryNode<T> node)
		{
			if (node == null)
				return true;

			var balanceFactor = Height(node.LeftChild) - Height(node.RightChild);

			return Math.Abs(balanceFactor) <= 1 &&
							IsBalanced(node.LeftChild) &&
							IsBalanced(node.RightChild);
		}
		#endregion
	}
}
