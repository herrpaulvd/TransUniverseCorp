using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
    public class ObjectKeeper
    {
        private readonly List<(object? obj, DateTime creation)> kept = [];

        private bool IsFree(int index)
            => index >= kept.Count || kept[index].obj is null || (DateTime.Now - kept[index].creation).TotalHours > 1;

        private void Set(int index, object obj)
        {
            while (kept.Count <= index) kept.Add(default);
            kept[index] = (obj, DateTime.Now);
        }

        public int Keep(object obj)
        {
            for (int i = 0; ; i++)
            {
                if (IsFree(i))
                {
                    Set(i, obj);
                    return i;
                }
            }
        }

        public object? Get(int index) => kept[index].obj;

        public void Free(int index) => kept[index] = default;

        private ObjectKeeper() { }

        public static readonly ObjectKeeper Instance = new();
    }
}
