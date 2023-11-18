using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TemporaryNewNoSQLdatabaseNamedAfterSweetLadyLuck
{
    public class CommonModel : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, string> self = new();

        public CommonModel() { }

        internal CommonModel(object o, RandomDb randomDb)
        {
            var t = o.GetType();
            var pgettable = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            foreach(var p in pgettable)
            {
                var val = p.GetValue(o, null);
                if (val is null) continue;
                var vt = val.GetType();
                if (
                    vt == randomDb.TString ||
                    vt == randomDb.TLong ||
                    vt == randomDb.TInt ||
                    vt == randomDb.TDate ||
                    vt == randomDb.TTime ||
                    vt == randomDb.TBool ||
                    vt == randomDb.TFloat)
                    self.Add(p.Name, val.ToString());
                else if(vt.IsEnum)
                {
                    var names = Enum.GetNames(vt);
                    var rawvalues = Enum.GetValues(vt);
                    int[] values = new int[rawvalues.Length];
                    for(int i = 0; i < rawvalues.Length; i++)
                    {
                        values[i] = (int)Convert.ChangeType(rawvalues.GetValue(i), randomDb.TInt);
                    }

                    int intval = (int)Convert.ChangeType(val, randomDb.TInt);
                    for(int i = 0; i < rawvalues.Length; i++)
                        if(intval == values[i])
                            self.Add(p.Name, names[i]);
                }
                else
                {
                    var pname = vt.GetProperty("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                    if (pname is not null)
                    {
                        self.Add(p.Name + "." + pname.Name, pname.GetValue(val).ToString());
                    }
                    else
                    {
                        pname = vt.GetProperty("ID", BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                        if (pname is not null)
                        {
                            self.Add(p.Name + "." + pname.Name, pname.GetValue(val).ToString());
                        }
                    }
                }
            }
        }

        public void Add(string key, string value)
        {
            self.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return self.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return self.GetEnumerator();
        }
    }
}
