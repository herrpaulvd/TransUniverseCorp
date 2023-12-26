using BL.Repos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL.ReposImpl
{
    // DAL repos

    internal class CustomerRepoImpl : ExtendedRepoImpl<BL.Customer, Entities.Customer>, ICustomerRepo
    { public CustomerRepoImpl() : base(context => context.Customers) { } }

    internal class DriverRepoImpl : ExtendedRepoImpl<BL.Driver, Entities.Driver>, IDriverRepo
    { public DriverRepoImpl() : base(context => context.Drivers) { } }

    internal class EdgeRepoImpl : UniversalRepoImpl<BL.Edge, Entities.Edge>, IEdgeRepo
    { public EdgeRepoImpl() : base(context => context.Edges) { } }

    internal class OrderRepoImpl : UniversalRepoImpl<BL.Order, Entities.Order>, IOrderRepo
    {
        public OrderRepoImpl() : base(context => context.Orders) { }

        public ICollection<Order> GetOrdersByCustomer(int customerID)
        {
            return entities.Where(e => e.Customer == customerID).AsEnumerable().Select(dalEntity =>
            {
                context.Entry(dalEntity).State = EntityState.Detached;
                return GetBLEntity(dalEntity)!;
            }).ToArray();
        }
    }

    internal class ScheduleElementRepoImpl : UniversalRepoImpl<BL.ScheduleElement, Entities.ScheduleElement>, IScheduleElementRepo
    { public ScheduleElementRepoImpl() : base(context => context.ScheduleElements) { } }

    internal class SpaceObjectRepoImpl : ExtendedRepoImpl<BL.SpaceObject, Entities.SpaceObject>, ISpaceObjectRepo
    { public SpaceObjectRepoImpl() : base(context => context.SpaceObjects) { } }

    internal class SpacePortRepoImpl : ExtendedRepoImpl<BL.SpacePort, Entities.SpacePort>, ISpacePortRepo
    { public SpacePortRepoImpl() : base(context => context.SpacePorts) { } }

    internal class SpaceshipRepoImpl : ExtendedRepoImpl<BL.Spaceship, Entities.Spaceship>, ISpaceshipRepo
    { public SpaceshipRepoImpl() : base(context => context.Spaceships) { } }

    internal class UserRepoImpl : UniversalRepoImpl<BL.User, Entities.User>, IUserRepo
    {
        public UserRepoImpl() : base(context => context.Users) { }

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
