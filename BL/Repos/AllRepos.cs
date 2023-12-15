using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repos
{
    public interface ICustomerRepo : IExtendedRepo<Customer> { }
    public interface IDriverRepo : IExtendedRepo<Driver> { }
    public interface IEdgeRepo : IUniversalRepo<Edge> { }
    public interface IOrderRepo : IUniversalRepo<Order> { }
    public interface IScheduleElementRepo : IUniversalRepo<ScheduleElement> { }
    public interface ISpaceObjectRepo : IExtendedRepo<SpaceObject> { }
    public interface ISpacePortRepo : IExtendedRepo<SpacePort> { }
    public interface ISpaceshipRepo : IExtendedRepo<Spaceship> { }
    public interface IUserRepo : IUniversalRepo<User> { }
}
