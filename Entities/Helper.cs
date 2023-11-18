using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal static class Helper
    {
        private static readonly string[] Classes =
        {
            "D", "C", "B", "A", "S"
        };

        private static readonly int ClassesCount = Classes.Length;
        private static readonly int UpperBound = 1 << ClassesCount;

        public static string Int2Classes(int n)
        {
            if (n >= UpperBound) throw new Exception();
            StringBuilder result = new();
            for(int i = 0; i < ClassesCount; i++)
            {
                if((n & (1 << i)) != 0)
                {
                    if (result.Length > 0) result.Append(';');
                    result.Append(Classes[i]);
                }
            }
            return result.ToString();
        }

        public static int Classes2Int(string s)
        {
            int result = 0;
            string[] classes = s.Split(';');
            foreach(string c in classes.Where(c => c.Length > 0))
            {
                int index = Array.IndexOf<string>(Classes, c);
                if (index == -1) throw new Exception();
                result |= (1 << index);
            }
            return result;
        }

        public static int State2Int(int? state) => state ?? -1;
        public static int? Int2State(int i) => i < 0 ? null : i;

        public static string Time2String(long time) => new DateTime(time).ToString();
        public static long String2Time(string s) => DateTime.Parse(s).Ticks;

        public static string Time2StringWnull(long? time) => time.HasValue ? new DateTime(time.Value).ToString() : "";
        public static long? String2TimeWnull(string s) => s.Length == 0 ? null : DateTime.Parse(s).Ticks;
    }
}
