using System;
using System.Collections;
using System.Collections.Generic;

namespace Colmillos
{
	/// <summary>
	/// A collection of modifiers that can be treated as a single modifier.
	/// </summary>
	[Serializable]
	public class ModifierCollection<T> : Modifier<T>, IModifierCollection<T> where T : class
	{
		private readonly List<IModifier<T>> list = new List<IModifier<T>>();

		/// <summary>
		/// Gets a representation of the modified object with all modifiers applied.
		/// </summary>
		public override T AsModified() => list.LastOrDefault()?.AsModified() ?? Target;

		/// <summary>
		/// Gets the number of modifiers in the collection.
		/// </summary>
		public int Count => list.Count;

		/// <summary>
		/// Gets or sets the modifier at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the modifier to get or set.</param>
		/// <returns>The modifier at the specified index.</returns>
		public IModifier<T> this[int index]
		{
			get => list[index];
			set
			{
				list[index] = value;
				RebindAtIndex(index);
			}
		}

		/// <summary>
		/// Binds the collection to an object.
		/// </summary>
		/// <param name="target">The entity to bind the collection to.</param>
		public override void Bind(T target)
		{
			// Set target
			base.Bind(target);

			// Rebind the entire collection in case of deserialization
			BindList();
		}

		/// <summary>
		/// Removes all modifiers from the collection.
		/// </summary>
		public void Clear() => list.Clear();

		/// <summary>
		/// Determines the index of a specific modifier in the collection.
		/// </summary>
		/// <param name="item">The modifier to locate in the collection.</param>
		/// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
		public int IndexOf(IModifier<T> item) => list.IndexOf(item);

		/// <summary>
		/// Inserts a modifier at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The modifier to insert into the collection.</param>
		public void Insert(int index, IModifier<T> item)
		{
			list.Insert(index, item);
			RebindAtIndex(index);
		}

		/// <summary>
		/// Adds a modifier to the collection.
		/// </summary>
		/// <param name="item">The modifier to add to the collection.</param>
		public void Add(IModifier<T> item)
		{
			list.Add(item);
			RebindAtIndex(Count - 1);
		}

		/// <summary>
		/// Determines whether a specific modifier is in the collection.
		/// </summary>
		/// <param name="item">The modifier to locate in the collection.</param>
		/// <returns><c>true</c> if <paramref name="item"/> is found in the collection; otherwise, <c>false</c>.</returns>
		public bool Contains(IModifier<T> item) => list.Contains(item);

		/// <summary>
		/// Removes a specific modifier from the collection.
		/// </summary>
		/// <param name="item">The modifier to remove from the collection.</param>
		/// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the collection; otherwise, <c>false</c>.</returns>
		public bool Remove(IModifier<T> item)
		{
			int index = list.FindIndex(m => m.Equals(item));

			if (index == -1) return false;

			RemoveAt(index);
			return true;
		}

		/// <summary>
		/// Removes the modifier at the specified index in the collection.
		/// </summary>
		/// <param name="index">The zero-based index of the modifier to remove.</param>
		public void RemoveAt(int index)
		{
			list.RemoveAt(index);

			if (Count > index)
			{
				RebindAtIndex(index);
			}
		}

		bool ICollection<IModifier<T>>.IsReadOnly => false;
		void ICollection<IModifier<T>>.CopyTo(IModifier<T>[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

		private IEnumerator<IModifier<T>> GetEnumerator() => list.GetEnumerator();
		IEnumerator<IModifier<T>> IEnumerable<IModifier<T>>.GetEnumerator() => GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private void BindList()
		{
			if (list.Count > 0)
			{
				// Bind first modifier in the collection to the target
				list[0].Bind(Target);

				// Rebind rest of collection
				for (int i = 1; i < list.Count; i++)
				{
					list[i].Bind(list[i - 1].AsModified);
				}
			}
		}

		private void RebindAtIndex(int index)
		{
			IModifier<T> item = list[index];

			if (index == 0)
			{
				item.Bind(Target);
			}
			else
			{
				item.Bind(list[index - 1].AsModified);
			}

			if (Count > index + 1)
			{
				list[index + 1].Bind(item.AsModified);
			}
		}
	}
}
