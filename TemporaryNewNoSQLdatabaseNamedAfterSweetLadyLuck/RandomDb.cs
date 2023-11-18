using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;

using Entities;

namespace TemporaryNewNoSQLdatabaseNamedAfterSweetLadyLuck
{
    public class RandomDb
    {
        //private List<Customer> Customers = new(); //+
        //private List<Driver> Drivers = new(); //+
        //private List<Edge> Edges = new(); //+
        //private List<Order> Orders = new(); //+
        //private List<ScheduleElement> ScheduleElements = new(); //+
        //private List<SpaceObject> SpaceObjects = new(); // ++
        //private List<SpacePort> SpacePorts = new();
        //private List<Spaceship> Spaceships = new(); // +
        //private List<User> Users = new(); // = admin + drivers + customers

        private RandomDb() { /*TryYourLuckAkaInitDatabase();*/ }

        public static readonly RandomDb Universe = new();

        private const int CntGalaxies = 3; //+
        private const int CntStarsPerGalaxy = 3; // +
        private const int CntPlanetsPerStar = 3; // +
        private const int CntPortsPerPlanet = 3; // +
        private const int CntDrivers = 10; // +
        private const int CntSpaceships = 10; // +
        private const int CntEdges = 30; // +
        private const int CntCustomers = 10; // +
        private const int CntOrders = 10; // +
        private const int CntScheduleElementsPerOrder = 10;

        private Random random = new();
        private char[] allowedChars =
             Enumerable.Range('0', 10)
            .Concat(Enumerable.Range('a', 26))
            .Concat(Enumerable.Range('A', 26))
            .Select(c => (char)c)
            .ToArray();

        private T Rand<T>(IList<T> container) => container[random.Next(container.Count)];

        private string MakeString()
        {
            int length = random.Next(10, 20);
            char[] chars = new char[length];
            for (int i = 0; i < chars.Length; i++)
                chars[i] = Rand(allowedChars);
            return new string(chars);
        }

        private int MakeInt() => random.Next();
        private long MakeLong() => random.NextInt64();
        private float MakeFloat() => random.NextSingle();
        private TimeSpan MakeTimeSpan() => new TimeSpan(MakeLong() % 1_000_000_000_000L);
        private DateTime MakeDateTime() => new DateTime(MakeLong() % 1_000_000_000_000_000L);
        private bool MakeBool() => random.Next(2) == 0;

        private object MakeEnum(Type type)
        {
            var values = Enum.GetValues(type);
            return values.GetValue(random.Next(values.Length));
        }

        internal readonly Type TString = typeof(string);
        internal readonly Type TInt = typeof(int);
        internal readonly Type TLong = typeof(long);
        internal readonly Type TFloat = typeof(float);
        internal readonly Type TTime = typeof(TimeSpan);
        internal readonly Type TDate = typeof(DateTime);
        internal readonly Type TBool = typeof(bool);

        private void InitObject(object o)
        {
            var t = o.GetType();
            var psettable = t.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
            foreach(var p in psettable)
            {
                var pt = p.PropertyType;
                if(pt == TString)
                    p.SetValue(o, MakeString());
                else if(pt == TInt)
                    p.SetValue(o, MakeInt());
                else if(pt == TLong)
                    p.SetValue(o, MakeLong());
                else if(pt == TFloat)
                    p.SetValue(o, MakeFloat());
                else if(pt == TDate)
                    p.SetValue(o, MakeDateTime());
                else if(pt == TTime)
                    p.SetValue(o, MakeTimeSpan());
                else if(pt.IsEnum)
                    p.SetValue(o, MakeEnum(pt));
                else if(pt == TBool)
                    p.SetValue(o, MakeBool());
            }
        }

        //private void TryYourLuckAkaInitDatabase()
        //{
        //    List<SpaceObject> planets = new();
        //    for(int i = 0; i < CntGalaxies; i++)
        //    {
        //        SpaceObject g = new();
        //        InitObject(g);
        //        g.Kind = SpaceObjectKind.Galaxy;
        //        g.SystemCenter = null;
        //        SpaceObjects.Add(g);
        //        for(int j = 0; j < CntStarsPerGalaxy; j++)
        //        {
        //            SpaceObject s = new();
        //            InitObject(s);
        //            s.Kind = SpaceObjectKind.Planet;
        //            s.SystemCenter = g;
        //            s.SystemPosition = j + 1;
        //            SpaceObjects.Add(s);
        //            for(int k = 0; k < CntPlanetsPerStar; k++)
        //            {
        //                SpaceObject p = new();
        //                InitObject(p);
        //                p.SystemCenter = s;
        //                p.SystemPosition = k + 1;
        //                SpaceObjects.Add(p);
        //                planets.Add(p);
        //                for(int l = 0; l < CntPortsPerPlanet; l++)
        //                {
        //                    SpacePort pp = new();
        //                    InitObject(pp);
        //                    pp.Planet = p;
        //                    SpacePorts.Add(pp);
        //                }
        //            }
        //        }
        //    }
        //
        //    for(int i = 0; i < CntEdges; i++)
        //    {
        //        Edge e = new();
        //        InitObject(e);
        //        e.Start = Rand(planets);
        //        e.End = Rand(planets);
        //        Edges.Add(e);
        //    }
        //
        //    for(int i = 0; i < CntDrivers; i++)
        //    {
        //        Driver d = new();
        //        InitObject(d);
        //        Drivers.Add(d);
        //    }
        //    
        //    for(int i = 0; i < CntSpaceships; i++)
        //    {
        //        Spaceship s = new();
        //        InitObject(s);
        //        Spaceships.Add(s);
        //    }
        //
        //    for(int i = 0; i < CntCustomers; i++)
        //    {
        //        Customer c = new();
        //        InitObject(c);
        //        Customers.Add(c);
        //    }
        //
        //    for(int i = 0; i < CntOrders; i++)
        //    {
        //        Order o = new();
        //        InitObject(o);
        //        o.LoadingPort = Rand(SpacePorts);
        //        o.UnloadingPort = Rand(SpacePorts);
        //        o.Driver = Rand(Drivers);
        //        o.Spaceship = Rand(Spaceships);
        //        Orders.Add(o);
        //
        //        for(int j = 0; j < CntScheduleElementsPerOrder; j++)
        //        {
        //            ScheduleElement e = new();
        //            InitObject(e);
        //            e.Driver = o.Driver;
        //            e.Spaceship = o.Spaceship;
        //            ScheduleElements.Add(e);
        //            e.Driver.CurrentState = e;
        //            e.Spaceship.CurrentState = e;
        //            o.CurrentState = e;
        //        }
        //    }
        //
        //    User admin = new();
        //    InitObject(admin);
        //    Users.Add(admin);
        //
        //    foreach(var d in Drivers)
        //    {
        //        User u = new();
        //        InitObject(u);
        //        u.Driver = d;
        //        Users.Add(u);
        //    }
        //
        //    foreach(var c in Customers)
        //    {
        //        User u = new();
        //        InitObject(u);
        //        u.Customer = c;
        //        Users.Add(u);
        //    }
        //}

        public CommonModel GetCustomer(int index) => null;
            //=> new(Customers[index], this);

        public CommonModel GetDriver(int index) => null;
        //{
        //    Driver d = Drivers[index];
        //    CommonModel m = new(d, this);
        //    m.Add("CurrentSpaceship", d?.CurrentState?.Spaceship?.Name ?? "-");
        //    m.Add("CurrentOrder", d?.CurrentState?.Order?.ID.ToString() ?? "-");
        //    return m;
        //}

        public CommonModel GetEdge(int index) => null;
            //=> new (Edges[index], this);

        public CommonModel GetOrder(int index) => null;
        //{
        //    Order o = Orders[index];
        //    CommonModel m = new(o, this);
        //    m.Add("CurrentDriver", o?.CurrentState?.Driver.Name ?? "-");
        //    m.Add("CurrentSpaceship", o?.CurrentState?.Spaceship?.Name ?? "-");
        //    return m;
        //}

        public CommonModel GetScheduleElement(int index) => null;
            //=> new (ScheduleElements[index], this);

        public CommonModel GetSpaceObject(int index) => null;
            //=> new (SpaceObjects[index], this);

        public CommonModel GetSpacePort(int index) => null;
        //=> new(SpacePorts[index], this);

        public CommonModel GetSpaceship(int index) => null;
        //{
        //    Spaceship s = Spaceships[index];
        //    CommonModel m = new(s, this);
        //    m.Add("CurrentDriver", s?.CurrentState?.Driver?.Name ?? "-");
        //    m.Add("CurrentOrder", s?.CurrentState?.Order?.ID.ToString() ?? "-");
        //    return m;
        //}

        public CommonModel GetUser(int index) => null;
            //=> new (Users[index], this);
    }
}
