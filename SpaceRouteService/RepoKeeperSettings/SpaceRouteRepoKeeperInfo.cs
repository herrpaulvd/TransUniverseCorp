using BL;

namespace SpaceRouteService.RepoKeeperSettings
{
    public class SpaceRouteRepoKeeperInfo : BL.RepoSettings.RepoKeeperInfo
    {
        public SpaceRouteRepoKeeperInfo()
        {
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
