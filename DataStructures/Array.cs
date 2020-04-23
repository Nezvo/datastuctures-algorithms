using System;
using System.Linq;

namespace DataStructures
{
  public class Array<T> where T : IComparable
	{
    #region Internals and properties
    private T[] items;

    public int Count { get; private set; }

    public Array(int size)
    {
      items = new T[size];
    }
    #endregion

    #region Public methods
    public void Insert(T item)
    {
      ResizeIfRequired();

      items[Count++] = item;
    }

    public void InsertAt(T item, int index)
    {
      if (index < 0 || index > Count)
        throw new ArgumentOutOfRangeException();

      ResizeIfRequired();

      for (int i = Count - 1; i >= index; i--)
        items[i + 1] = items[i];

      items[index] = item;
      Count++;
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= Count)
        throw new ArgumentOutOfRangeException();

      for (int i = index; i < Count; i++)
        items[i] = items[i + 1];

      Count--;
    }

    public int IndexOf(T item)
    {
      for (int i = 0; i < Count; i++)
        if (items[i].CompareTo(item) == 0)
          return i;

      return -1;
    }

    public void Reverse()
    {
      T[] newItems = new T[Count];

      for (int i = 0; i < Count; i++)
        newItems[i] = items[Count - i - 1];

      items = newItems;
    }

    public T Max()
    {
      T max = items.FirstOrDefault();

      foreach (var item in items)
        if (item.CompareTo(max)> 0)
          max = item;

      return max;
    }

    public Array<T> Intersect(Array<T> other)
    {
      var intersection = new Array<T>(Count);

      foreach (var item in items)
        if (other.IndexOf(item) >= 0)
          intersection.Insert(item);

      return intersection;
    }

    public override string ToString()
    {
      return $"[{string.Join(", ", items.Take(Count))}]";
    }

    #endregion

    #region Private methods

    private void ResizeIfRequired()
    {
      if (items.Length == Count)
      {
        T[] newItems = new T[Count * 2];

        for (int i = 0; i < Count; i++)
          newItems[i] = items[i];

        items = newItems;
      }
    }
    #endregion
  }
}
