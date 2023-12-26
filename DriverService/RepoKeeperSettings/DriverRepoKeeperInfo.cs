using BL;

namespace DriverService.RepoKeeperSettings
{
    public class DriverRepoKeeperInfo : BL.RepoSettings.RepoKeeperInfo
    {
        public DriverRepoKeeperInfo()
        {
            // spaceroute
            AddPresetRepo("Edge", ServiceAddress.SpaceRoute + "/edge");
            AddPresetRepo("Order", ServiceAddress.SpaceRoute + "/order");
            AddPresetRepo("ScheduleElement", ServiceAddress.SpaceRoute + "/schedule");
            AddPresetRepo("SpaceObject", ServiceAddress.SpaceRoute + "/spaceobject");
            AddPresetRepo("SpacePort", ServiceAddress.SpaceRoute + "/spaceport");

            // customer
            AddPresetRepo("Customer", ServiceAddress.Customer + "/customer");

            // user
            AddPresetRepo("User", ServiceAddress.User + "/user");
        }
    }
}
