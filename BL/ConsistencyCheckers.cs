using BL.Repos;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal static class ConsistencyCheckers
    {
        private static bool Check<TEntity>(int? fk, IUniversalRepo<TEntity> repo)
            where TEntity : IBLEntity
            => fk is null || repo.Get(fk.Value) is not null;

        public static bool CheckCustomer(this int? fk) => Check(fk, RepoKeeper.Instance.CustomerRepo);
        public static bool CheckCustomer(this int fk) => Check(fk, RepoKeeper.Instance.CustomerRepo);
        public static bool CheckDriver(this int? fk) => Check(fk, RepoKeeper.Instance.DriverRepo);
        public static bool CheckDriver(this int fk) => Check(fk, RepoKeeper.Instance.DriverRepo);
        public static bool CheckEdge(this int? fk) => Check(fk, RepoKeeper.Instance.EdgeRepo);
        public static bool CheckEdge(this int fk) => Check(fk, RepoKeeper.Instance.EdgeRepo);
        public static bool CheckOrder(this int? fk) => Check(fk, RepoKeeper.Instance.OrderRepo);
        public static bool CheckOrder(this int fk) => Check(fk, RepoKeeper.Instance.OrderRepo);
        public static bool CheckScheduleElement(this int? fk) => Check(fk, RepoKeeper.Instance.ScheduleElementRepo);
        public static bool CheckScheduleElement(this int fk) => Check(fk, RepoKeeper.Instance.ScheduleElementRepo);
        public static bool CheckSpaceObject(this int? fk) => Check(fk, RepoKeeper.Instance.SpaceObjectRepo);
        public static bool CheckSpaceObject(this int fk) => Check(fk, RepoKeeper.Instance.SpaceObjectRepo);
        public static bool CheckSpacePort(this int? fk) => Check(fk, RepoKeeper.Instance.SpacePortRepo);
        public static bool CheckSpacePort(this int fk) => Check(fk, RepoKeeper.Instance.SpacePortRepo);
        public static bool CheckSpaceship(this int? fk) => Check(fk, RepoKeeper.Instance.SpaceshipRepo);
        public static bool CheckSpaceship(this int fk) => Check(fk, RepoKeeper.Instance.SpaceshipRepo);
        public static bool CheckUser(this int? fk) => Check(fk, RepoKeeper.Instance.UserRepo);
        public static bool CheckUser(this int fk) => Check(fk, RepoKeeper.Instance.UserRepo);
    
        private static bool CheckOnDelete<TEntity>(int? fk, IUniversalRepo<TEntity> repo, Func<TEntity, int?> property)
            where TEntity : IBLEntity
        {
            if (fk is null) return true;
            foreach(var entity in repo.GetAll())
            {
                int? propval = property(entity);
                if (propval is not null && propval == fk) return false;
            }
            return true;
        }

        public static bool CheckCustomerOnDelete(this int? fk, Func<Customer, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.CustomerRepo, property);
        public static bool CheckCustomerOnDelete(this int fk, Func<Customer, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.CustomerRepo, property);
        public static bool CheckDriverOnDelete(this int? fk, Func<Driver, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.DriverRepo, property);
        public static bool CheckDriverOnDelete(this int fk, Func<Driver, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.DriverRepo, property);
        public static bool CheckEdgeOnDelete(this int? fk, Func<Edge, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.EdgeRepo, property);
        public static bool CheckEdgeOnDelete(this int fk, Func<Edge, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.EdgeRepo, property);
        public static bool CheckOrderOnDelete(this int? fk, Func<Order, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.OrderRepo, property);
        public static bool CheckOrderOnDelete(this int fk, Func<Order, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.OrderRepo, property);
        public static bool CheckScheduleElementOnDelete(this int? fk, Func<ScheduleElement, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.ScheduleElementRepo, property);
        public static bool CheckScheduleElementOnDelete(this int fk, Func<ScheduleElement, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.ScheduleElementRepo, property);
        public static bool CheckSpaceObjectOnDelete(this int? fk, Func<SpaceObject, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpaceObjectRepo, property);
        public static bool CheckSpaceObjectOnDelete(this int fk, Func<SpaceObject, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpaceObjectRepo, property);
        public static bool CheckSpacePortOnDelete(this int? fk, Func<SpacePort, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpacePortRepo, property);
        public static bool CheckSpacePortOnDelete(this int fk, Func<SpacePort, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpacePortRepo, property);
        public static bool CheckSpaceshipOnDelete(this int? fk, Func<Spaceship, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpaceshipRepo, property);
        public static bool CheckSpaceshipOnDelete(this int fk, Func<Spaceship, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.SpaceshipRepo, property);
        public static bool CheckUserOnDelete(this int? fk, Func<User, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.UserRepo, property);
        public static bool CheckUserOnDelete(this int fk, Func<User, int?> property) =>
            CheckOnDelete(fk, RepoKeeper.Instance.UserRepo, property);
    }
}
