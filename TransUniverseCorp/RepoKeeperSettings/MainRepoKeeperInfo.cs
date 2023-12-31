﻿using BL;

namespace TransUniverseCorp.RepoKeeperSettings
{
    public class MainRepoKeeperInfo : BL.RepoSettings.RepoKeeperInfo
    {
        public MainRepoKeeperInfo()
        {
            // spaceroute
            AddPresetRepo("Edge", ServiceAddress.SpaceRoute + "/edge");
            AddPresetRepo("Order", ServiceAddress.SpaceRoute + "/order");
            AddPresetRepo("ScheduleElement", ServiceAddress.SpaceRoute + "/schedule");
            AddPresetRepo("SpaceObject", ServiceAddress.SpaceRoute + "/spaceobject");
            AddPresetRepo("SpacePort", ServiceAddress.SpaceRoute + "/spaceport");

            // customer
            AddPresetRepo("Customer", ServiceAddress.Customer + "/customer");

            // driver
            AddPresetRepo("Driver", ServiceAddress.Driver + "/driver");
            AddPresetRepo("Spaceship", ServiceAddress.Driver + "/spaceship");

            // user
            AddPresetRepo("User", ServiceAddress.User + "/user");
        }
    }
}
