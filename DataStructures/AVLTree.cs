using DataStructures.Interfaces;
using System;

namespace DataStructures
{
	/// <summary>
	/// AVL Tree implementation.
	/// Object that will be added to the tree must implement IBinaryNode
	/// </summary>
	/// <typeparam name="T">Type of Id property in an object that will be added to the tree</typeparam>
	public class AVLTree<T> where T : IComparable
	{
		#region Internals and properties
		private IBinaryNode<T> root;
		#endregion

		#region Public methods
		public void Add(IBinaryNode<T> node)
		{
			root = Add(root, node);
		} 
		#endregion

		#region Private methods
		private IBinaryNode<T> Add(IBinaryNode<T> root, IBinaryNode<T> node)
		{
			if (root == null)
				return node;

			if (node.Id.CompareTo(root.Id) < 0)
				root.LeftChild = Add(root.LeftChild, node);
			else
				root.RightChild = Add(root.RightChild, node);

			SetHeight(root);

			return Balance(root);
		}

		private IBinaryNode<T> Balance(IBinaryNode<T> root)
		{
			if (IsLeftHeavy(root))
			{
				if (BalanceFactor(root.LeftChild) < 0)
					root.LeftChild = RotateLeft(root.LeftChild);
				return RotateRight(root);
			}
			else if (IsRightHeavy(root))
			{
				if (BalanceFactor(root.RightChild) > 0)
					root.RightChild = RotateRight(root.RightChild);
				return RotateLeft(root);
			}
			return root;
		}

		private IBinaryNode<T> RotateLeft(IBinaryNode<T> root)
		{
			var newRoot = root.RightChild;

			root.RightChild = newRoot.LeftChild;
			newRoot.LeftChild = root;

			SetHeight(root);
			SetHeight(newRoot);

			return newRoot;
		}

		private IBinaryNode<T> RotateRight(IBinaryNode<T> root)
		{
			var newRoot = root.LeftChild;

			root.LeftChild = newRoot.RightChild;
			newRoot.RightChild = root;

			SetHeight(root);
			SetHeight(newRoot);

			return newRoot;
		}

		private void SetHeight(IBinaryNode<T> node)
		{
			node.Height = Math.Max(
							Height(node.LeftChild),
							Height(node.RightChild)) + 1;
		}

		private bool IsLeftHeavy(IBinaryNode<T> node) => BalanceFactor(node) > 1;

		private bool IsRightHeavy(IBinaryNode<T> node) => BalanceFactor(node) < -1;

		private int BalanceFactor(IBinaryNode<T> node) => (node == null) ? 0 : Height(node.LeftChild) - Height(node.RightChild);

		private int Height(IBinaryNode<T> node) => (node == null) ? -1 : node.Height;
		#endregion
	}
}
