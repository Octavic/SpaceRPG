using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Utility
{
    public interface IReadOnlyList<T> : IList<T>
    {
        new void Insert(int index, T item);

        new void RemoveAt(int index);

        new void Add(T item);

        new void Clear();

        new bool Remove(T item);

    }
}
