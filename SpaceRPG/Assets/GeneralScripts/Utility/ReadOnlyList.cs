using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GeneralScripts.Utility
{
    public class ReadOnlyList<T> : List<T>, IReadOnlyList<T>
    {
    }
}
