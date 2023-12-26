using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace UnviersalMV
{
    public class CommonModel
    {
        private readonly object origin;
        private readonly Dictionary<string, PropertyInfo> get = [];
        private readonly Dictionary<string, PropertyInfo> getSimple = [];
        private readonly Dictionary<string, PropertyInfo> set = [];

        private static bool IsHidden(PropertyInfo p) => p.GetCustomAttribute<HiddenAttribute>() is not null;

        private static bool IsString(PropertyInfo p) => p.PropertyType == typeof(string);

        private static bool IsBool(PropertyInfo p) => p.PropertyType == typeof(bool);

        private static bool IsInt(PropertyInfo p)
        {
            var t = p.PropertyType;
            return t == typeof(int) || t == typeof(long);
        }

        private static bool IsFloat(PropertyInfo p)
        {
            var t = p.PropertyType;
            return t == typeof(float) || t == typeof(double);
        }

        private static bool CheckType(PropertyInfo p)
            => IsString(p) || IsBool(p) || IsInt(p) || IsFloat(p);

        public CommonModel(object origin)
        {
            this.origin = origin;

            var t = origin.GetType();
            foreach(var p in t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!p.CanRead || !CheckType(p) || p.GetCustomAttribute<NoPassAttribute>() is not null)
                    continue;
                var name = p.GetCustomAttribute<WithNameAttribute>()?.Name ?? p.Name;
                get.Add(name, p);
                if(p.GetCustomAttribute<PassSimpleAttribute>() is not null)
                    getSimple.Add(name, p);
                if(p.CanWrite && p.GetCustomAttribute<PassGetOnlyAttribute>() is null)
                    set.Add(name, p);
            }
        }

        public static CommonModel Create<T>() where T : new() => new(new T());

        public struct PropertyValue
        {
            public string Name;
            public string Value;
            public string Type;
            public string Step;

            public PropertyValue(string name, string value, string type, string step)
            {
                Name = name;
                Value = value;
                Type = type;
                Step = step;
            }
        }

        private static IList<PropertyValue> GetValues(Dictionary<string, PropertyInfo> properties, object origin)
        {
            var result = new List<PropertyValue>();
            foreach (var kv in properties)
            {
                var name = kv.Key;
                var p = kv.Value;
                string type, step;
                if (IsHidden(p))
                    (type, step) = ("hidden", "1");
                else if (IsString(p))
                    (type, step) = ("text", "1");
                else if (IsBool(p))
                    (type, step) = ("checkbox", "1");
                else if (IsInt(p))
                    (type, step) = ("number", "1");
                else if (IsFloat(p))
                    (type, step) = ("number", "0.001");
                else
                    throw new Exception("Internal error");
                var svalue = p.GetValue(origin)?.ToString() ?? "";
                if (IsFloat(p))
                    svalue = svalue.Replace(',', '.');
                result.Add(new PropertyValue(name, svalue, type, step));
            }
            return result;
        }

        private static IList<PropertyValue> GetDefaultValues(Dictionary<string, PropertyInfo> properties, object origin)
        {
            var result = new List<PropertyValue>();
            foreach (var kv in properties)
            {
                var name = kv.Key;
                var p = kv.Value;
                string type, step, value;
                if (IsHidden(p))
                    (type, step, value) = ("hidden", "1", "0");
                else if (IsString(p))
                    (type, step, value) = ("text", "1", "");
                else if (IsBool(p))
                    (type, step, value) = ("checkbox", "1", "false");
                else if (IsInt(p))
                    (type, step, value) = ("number", "1", "0");
                else if (IsFloat(p))
                    (type, step, value) = ("number", "0.001", "0");
                else
                    throw new Exception("Internal error");
                result.Add(new PropertyValue(name, value, type, step));
            }
            return result;
        }

        public IList<PropertyValue> GetAllValues() => GetValues(get, origin);
        public IList<PropertyValue> GetSimpleValues() => GetValues(getSimple, origin);
        public IList<PropertyValue> GetSettableValues() => GetValues(set, origin);
        public IList<PropertyValue> GetDefaultSettableValues() => GetDefaultValues(set, origin);

        public bool SetValue(string name, string value)
        {
            if(set.TryGetValue(name, out PropertyInfo? p))
            {
                try
                {
                    if (IsFloat(p!)) value = value.Replace('.', ',');
                    var conv = Convert.ChangeType(value, p.PropertyType);
                    p.SetValue(origin, conv);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
    }
}
