using BL;

namespace TransUniverseCorp.RepoKeeperSettings
{
    public class MainRepoKeeperInfo : BL.RepoSettings.RepoKeeperInfo
    {
        public MainRepoKeeperInfo()
        {
            AddPresetRepo("Edge", ServiceAddress.SpaceRoute + "/edge");
            AddPresetRepo("ScheduleElement", ServiceAddress.SpaceRoute + "/schedule");
            AddPresetRepo("SpaceObject", ServiceAddress.SpaceRoute + "/spaceobject");
            AddPresetRepo("SpacePort", ServiceAddress.SpaceRoute + "/spaceport");
        }
    }
}
