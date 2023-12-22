using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReposImpl
{
    internal abstract class BaseUniversalRepoImpl
    {
        private static Dictionary<Type, IList<(string name, PropertyInfo info)>> properties = new();

        private static IList<(string name, PropertyInfo info)> GetProperties<T>()
        {
            var t = typeof(T);
            lock (properties)
            {
                if (!properties.TryGetValue(t, out var result))
                {
                    result = t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(p => p.CanRead && p.CanWrite)
                        .Select(p => (p.Name, p))
                        .ToArray();
                    properties.Add(t, result);
                }
                return result;
            }
        }

        private static Dictionary<(Type TInput, Type TOutput), IList<(PropertyInfo pin, PropertyInfo pout)>>
            propertyMatches = new();

        private static IList<(PropertyInfo pin, PropertyInfo pout)> GetMatch<TInput, TOutput>()
        {
            lock (propertyMatches)
            {
                var ttuple = (typeof(TInput), typeof(TOutput));
                if (!propertyMatches.TryGetValue(ttuple, out var result))
                {
                    var pall =
                        GetProperties<TInput>().Select(tuple => (tuple.name, 0, tuple.info))
                        .Concat(GetProperties<TOutput>().Select(tuple => (tuple.name, 1, tuple.info)))
                        .ToArray();
                    Array.Sort(pall);
                    result = new List<(PropertyInfo pin, PropertyInfo pout)>();
                    for (int i = 0, j = 1; j < pall.Length; i++, j++)
                        if (pall[i].name == pall[j].name
                            && pall[i].info.PropertyType == pall[j].info.PropertyType)
                            result.Add((pall[i].info, pall[j].info));
                    propertyMatches.Add(ttuple, result);
                }
                return result;
            }
        }

        protected static TOutput? Convert<TInput, TOutput>(TInput? input)
            where TOutput : class, new()
        {
            if (input is null) return null;
            var match = GetMatch<TInput, TOutput>();
            TOutput output = new();
            foreach (var (pin, pout) in match)
                pout.SetValue(output, pin.GetValue(input));
            return output;
        }
    }
}
