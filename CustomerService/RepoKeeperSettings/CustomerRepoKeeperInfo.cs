using BL;

namespace CustomerService.RepoKeeperSettings
{
    public class CustomerRepoKeeperInfo : BL.RepoSettings.RepoKeeperInfo
    {
        public CustomerRepoKeeperInfo()
        {
            // spaceroute
            AddPresetRepo("Edge", ServiceAddress.SpaceRoute + "/edge");
            AddPresetRepo("Order", ServiceAddress.SpaceRoute + "/order");
            AddPresetRepo("ScheduleElement", ServiceAddress.SpaceRoute + "/schedule");
            AddPresetRepo("SpaceObject", ServiceAddress.SpaceRoute + "/spaceobject");
            AddPresetRepo("SpacePort", ServiceAddress.SpaceRoute + "/spaceport");

            // driver
            AddPresetRepo("Driver", ServiceAddress.Driver + "/driver");
            AddPresetRepo("Spaceship", ServiceAddress.Driver + "/spaceship");

            // user
            AddPresetRepo("User", ServiceAddress.User + "/user");
        }
    }
}
