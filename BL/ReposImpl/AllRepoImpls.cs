using BL.Repos;
using CustomerDAL;
using DriverDAL;
using Microsoft.EntityFrameworkCore;
using SpaceRouteDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UserDAL;

namespace BL.ReposImpl
{
    // DAL repos

    internal class CustomerRepoImpl : ExtendedRepoImpl<BL.Customer, CustomerDAL.Customer>, ICustomerRepo
    { public CustomerRepoImpl() : base(new CustomerDbContext(), context => ((CustomerDbContext)context).Customers) { } }

    internal class DriverRepoImpl : ExtendedRepoImpl<BL.Driver, DriverDAL.Driver>, IDriverRepo
    { public DriverRepoImpl() : base(new DriverDbContext(), context => ((DriverDbContext)context).Drivers) { } }

    internal class EdgeRepoImpl : UniversalRepoImpl<BL.Edge, SpaceRouteDAL.Edge>, IEdgeRepo
    { public EdgeRepoImpl() : base(new SpaceRouteDbContext(), context => ((SpaceRouteDbContext)context).Edges) { } }

    internal class OrderRepoImpl : UniversalRepoImpl<BL.Order, SpaceRouteDAL.Order>, IOrderRepo
    {
        public OrderRepoImpl() : base(new SpaceRouteDbContext(), context => ((SpaceRouteDbContext)context).Orders) { }

        public ICollection<Order> GetOrdersByCustomer(int customerID)
        {
            return entities.Where(e => e.Customer == customerID).AsEnumerable().Select(dalEntity =>
            {
                context.Entry(dalEntity).State = EntityState.Detached;
                return GetBLEntity(dalEntity)!;
            }).ToArray();
        }
    }

    internal class ScheduleElementRepoImpl : UniversalRepoImpl<BL.ScheduleElement, SpaceRouteDAL.ScheduleElement>, IScheduleElementRepo
    { public ScheduleElementRepoImpl() : base(new SpaceRouteDbContext(), context => ((SpaceRouteDbContext)context).ScheduleElements) { } }

    internal class SpaceObjectRepoImpl : ExtendedRepoImpl<BL.SpaceObject, SpaceRouteDAL.SpaceObject>, ISpaceObjectRepo
    { public SpaceObjectRepoImpl() : base(new SpaceRouteDbContext(), context => ((SpaceRouteDbContext)context).SpaceObjects) { } }

    internal class SpacePortRepoImpl : ExtendedRepoImpl<BL.SpacePort, SpaceRouteDAL.SpacePort>, ISpacePortRepo
    { public SpacePortRepoImpl() : base(new SpaceRouteDbContext(), context => ((SpaceRouteDbContext)context).SpacePorts) { } }

    internal class SpaceshipRepoImpl : ExtendedRepoImpl<BL.Spaceship, DriverDAL.Spaceship>, ISpaceshipRepo
    { public SpaceshipRepoImpl() : base(new DriverDbContext(), context => ((DriverDbContext)context).Spaceships) { } }

    internal class UserRepoImpl : UniversalRepoImpl<BL.User, UserDAL.User>, IUserRepo
    {
        public UserRepoImpl() : base(new UserDbContext(), context => ((UserDbContext)context).Users) { }

        public User? FindByLogin(string login)
        {
            var dalEntity = entities.FirstOrDefault(e => e.Login == login);
            if (dalEntity is not null) context.Entry(dalEntity).State = EntityState.Detached;
            return GetBLEntity(dalEntity);
        }
    }

    // WEB repos
    internal class CustomerWebRepoImpl : ExtendedWebRepoImpl<BL.Customer>, ICustomerRepo
    { public CustomerWebRepoImpl(string url) : base(url) { } }

    internal class DriverWebRepoImpl : ExtendedWebRepoImpl<BL.Driver>, IDriverRepo
    { public DriverWebRepoImpl(string url) : base(url) { } }

    internal class EdgeWebRepoImpl : UniversalWebRepoImpl<BL.Edge>, IEdgeRepo
    { public EdgeWebRepoImpl(string url) : base(url) { } }

    internal class OrderWebRepoImpl : UniversalWebRepoImpl<BL.Order>, IOrderRepo
    {
        public OrderWebRepoImpl(string url) : base(url) { }

        public ICollection<Order> GetOrdersByCustomer(int customerID)
        {
            return PullArray<Order>($"ordersbycustomer/{customerID}")!;
        }
    }

    internal class ScheduleElementWebRepoImpl : UniversalWebRepoImpl<BL.ScheduleElement>, IScheduleElementRepo
    { public ScheduleElementWebRepoImpl(string url) : base(url) { } }

    internal class SpaceObjectWebRepoImpl : ExtendedWebRepoImpl<BL.SpaceObject>, ISpaceObjectRepo
    { public SpaceObjectWebRepoImpl(string url) : base(url) { } }

    internal class SpacePortWebRepoImpl : ExtendedWebRepoImpl<BL.SpacePort>, ISpacePortRepo
    { public SpacePortWebRepoImpl(string url) : base(url) { } }

    internal class SpaceshipWebRepoImpl : ExtendedWebRepoImpl<BL.Spaceship>, ISpaceshipRepo
    { public SpaceshipWebRepoImpl(string url) : base(url) { } }

    internal class UserWebRepoImpl : UniversalWebRepoImpl<BL.User>, IUserRepo
    {
        public UserWebRepoImpl(string url) : base(url) { }

        public User? FindByLogin(string login)
        {
            User result = new() { Id = INVALID_ID };
            if (!Pull(result, $"findbylogin/{login}"))
                return null;
            if (result.Id == INVALID_ID) return null;
            return result;
        }
    }
}
