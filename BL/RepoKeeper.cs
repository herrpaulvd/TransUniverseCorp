using BL.Repos;
using BL.RepoSettings;
using BL.ReposImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public sealed class RepoKeeper
    {
        private ICustomerRepo? customerImpl;
        private IDriverRepo? driverImpl;
        private IEdgeRepo? edgeRepoImpl;
        private IOrderRepo? orderRepoImpl;
        private IScheduleElementRepo? scheduleElementRepoImpl;
        private ISpaceObjectRepo? spaceObjectRepoImpl;
        private ISpacePortRepo? spacePortRepoImpl;
        private ISpaceshipRepo? spaceshipRepoImpl;
        private IUserRepo? userRepoImpl;

        public ICustomerRepo CustomerRepo => customerImpl ??= new CustomerRepoImpl();
        public IDriverRepo DriverRepo => driverImpl ??= new DriverRepoImpl();
        public IEdgeRepo EdgeRepo => edgeRepoImpl ??= new EdgeRepoImpl();
        public IOrderRepo OrderRepo => orderRepoImpl ??= new OrderRepoImpl();
        public IScheduleElementRepo ScheduleElementRepo => scheduleElementRepoImpl ??= new ScheduleElementRepoImpl();
        public ISpaceObjectRepo SpaceObjectRepo => spaceObjectRepoImpl ??= new SpaceObjectRepoImpl();
        public ISpacePortRepo SpacePortRepo => spacePortRepoImpl ??= new SpacePortRepoImpl();
        public ISpaceshipRepo SpaceshipRepo => spaceshipRepoImpl ??= new SpaceshipRepoImpl();
        public IUserRepo UserRepo => userRepoImpl ??= new UserRepoImpl();

        private RepoKeeper()
        {
            var executingAssembly = Assembly.GetEntryAssembly();
            if (executingAssembly is null) return;
            var currentAssembly = typeof(RepoKeeper).Assembly;
            var RKType = executingAssembly.GetTypes().FirstOrDefault(
                t => t.IsSubclassOf(typeof(RepoKeeperInfo))
                && t.GetConstructors().Any(c => c.GetParameters().Length == 0));
            if (RKType is null) return;
            var repoKeeperInfo = (RepoKeeperInfo)Activator.CreateInstance(RKType)!;
            var fields = typeof(RepoKeeper)
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                //.Where(f => f.Name.EndsWith("Impl"))
                .ToDictionary(f => f.FieldType.Name);
            foreach(var (entity, url) in repoKeeperInfo.PresetRepos)
            {
                var suffix = url is null ? "RepoImpl" : "WebRepoImpl";
                var entityRepoTypeName = $"BL.ReposImpl.{entity}{suffix}";
                var interfaceName = $"I{entity}Repo";
                var repoType = currentAssembly.GetType(entityRepoTypeName)!;
                fields[interfaceName].SetValue(
                    this, url is null
                    ? Activator.CreateInstance(repoType)
                    : Activator.CreateInstance(repoType, url));
            }
        }

        [ThreadStatic]
        private static RepoKeeper? instance;
        public static RepoKeeper Instance => instance ??= new();
    }
}
