using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RepoSettings
{
    public class RepoKeeperInfo
    {
        internal List<(Type, string?)> PresetRepos = new();

        protected void AddPresetRepo<T>(string? url = null)
            => PresetRepos.Add((typeof(T), url));
    }
}
