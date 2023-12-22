using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.RepoSettings
{
    public class RepoKeeperInfo
    {
        internal List<(string, string?)> PresetRepos = new();

        protected void AddPresetRepo(string entity, string? url = null)
            => PresetRepos.Add((entity, url));
    }
}
