using BL.Repos;
using BL.ReposImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class RepoKeeper
    {
        private CustomerRepoImpl? customerImpl;
        private DriverRepoImpl? driverImpl;
        private EdgeRepoImpl? edgeRepoImpl;
        private OrderRepoImpl? orderRepoImpl;
        private ScheduleElementRepoImpl? scheduleElementRepoImpl;
        private SpaceObjectRepoImpl? spaceObjectRepoImpl;
        private SpacePortRepoImpl? spacePortRepoImpl;
        private SpaceshipRepoImpl? spaceshipRepoImpl;
        private UserRepoImpl? userRepoImpl;

        public ICustomerRepo CustomerRepo => customerImpl ??= new();
        public IDriverRepo DriverRepo => driverImpl ??= new();
        public IEdgeRepo EdgeRepo => edgeRepoImpl ??= new();
        public IOrderRepo OrderRepo => orderRepoImpl ??= new();
        public IScheduleElementRepo ScheduleElementRepo => scheduleElementRepoImpl ??= new();
        public ISpaceObjectRepo SpaceObjectRepo => spaceObjectRepoImpl ??= new();
        public ISpacePortRepo SpacePortRepo => spacePortRepoImpl ??= new();
        public ISpaceshipRepo SpaceshipRepo => spaceshipRepoImpl ??= new();
        public IUserRepo UserRepo => userRepoImpl ??= new();

        private RepoKeeper() { }

        [ThreadStatic]
        private static RepoKeeper? instance;
        public static RepoKeeper Instance => instance ??= new();
    }
}
