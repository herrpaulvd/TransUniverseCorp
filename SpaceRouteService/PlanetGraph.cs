using BL;
using BL.Repos;

namespace SpaceRouteService
{
    internal static class PlanetGraph
    {
        private static long BuildCost(List<ScheduleElement> elements, long hiringCost, long usageCost)
        {
            long result = 0;
            foreach (ScheduleElement element in elements)
            {
                TimeSpan timeSpan = new(element.Time);
                long sec = (long)timeSpan.TotalSeconds;
                if (element.Driver is not null) result += hiringCost * sec;
                if (element.Spaceship is not null) result += usageCost * sec;
            }
            return result;
        }

        private static bool Dijkstra(
            Dictionary<int, List<Edge>> edges,
            int start, int end,
            List<Edge> output)
        {
            const long inf = long.MaxValue;
            Dictionary<int, long> d = [];
            d.Add(start, 0);
            foreach (var p in edges.Keys)
                if (p != start)
                    d.Add(p, inf);
            Dictionary<int, Edge> lastEdge = [];
            SortedSet<(long, int)> queue = new(d.Select(kv => (kv.Value, kv.Key)));
            while (queue.Count > 0)
            {
                var (dist, planet) = queue.Min;
                if (dist == inf)
                    return false;
                if (planet == end)
                {
                    while (planet != start)
                    {
                        var e = lastEdge[planet];
                        output.Add(e);
                        planet = e.Start;
                    }
                    output.Reverse();
                    return true;
                }
                queue.Remove((dist, planet));

                foreach (var e in edges[planet])
                {
                    var nextp = e.End;
                    var currdist = d[nextp];
                    var newdist = dist + e.Time;
                    if (newdist < currdist)
                    {
                        queue.Remove((currdist, nextp));
                        queue.Add((newdist, nextp));
                        if (!lastEdge.TryAdd(nextp, e))
                            lastEdge[nextp] = e;
                    }
                }
            }
            return false;
        }

        private static void BuildSchedulePart(
            int? driver,
            int? spaceship,
            long currentTime,
            long? endTime,
            int endPlanet,
            List<Edge> edges,
            Dictionary<int, int> planet2port,
            List<ScheduleElement> output)
        {
            foreach (var e in edges)
            {
                currentTime += e.Time;
                output.Add(new()
                {
                    Time = e.Time,
                    PlannedDepartureOrArrival = currentTime,
                    Driver = driver,
                    Spaceship = spaceship,
                    DestinationOrStop = planet2port[e.End],
                    IsStop = false
                });
            }
            if (endTime.HasValue)
            {
                long waitingEnd = Math.Max(endTime.Value, currentTime);
                long waitingTime = waitingEnd - currentTime;
                if (waitingTime > 0)
                    output.Add(new()
                    {
                        Time = waitingTime,
                        PlannedDepartureOrArrival = waitingEnd,
                        Driver = driver,
                        Spaceship = spaceship,
                        DestinationOrStop = planet2port[endPlanet],
                        IsStop = true,
                    });
            }
        }

        private static bool BuildScheduleElements(
            int driver,
            int spaceship,
            Dictionary<int, int> planet2Port,
            Dictionary<int, Edge> edges,
            int driverLocation,
            int spaceshipLocation,
            int loadingLocation,
            int end,
            long currentTime,
            long startTime,
            long endTime,
            int qualficationMask,
            int spaceshipMask,
            List<ScheduleElement> output)
        {
            Thread.Sleep(30_000);
            Dictionary<int, List<Edge>> graph = [];
            Dictionary<int, List<Edge>> fullgraph = [];
            foreach (var p in planet2Port.Keys)
            {
                graph.Add(p, []);
                fullgraph.Add(p, []);
            }
            foreach (var e in edges.Values)
            {
                fullgraph[e.Start].Add(e);
                if (((e.QualificationClasses & qualficationMask) != 0) && ((e.SpaceshipClasses & spaceshipMask)) != 0)
                    graph[e.Start].Add(e);
            }

            List<Edge> edgebuffer = [];
            var planets = planet2Port.Values;
            if (!Dijkstra(fullgraph, driverLocation, spaceshipLocation, edgebuffer))
                return false;
            BuildSchedulePart(driver, null, currentTime, null, spaceshipLocation, edgebuffer, planet2Port, output);
            long nextTime = output.Count == 0 ? currentTime : output[^1].PlannedDepartureOrArrival!.Value;
            if (nextTime > startTime)
                return false;
            edgebuffer.Clear();
            if (!Dijkstra(graph, spaceshipLocation, loadingLocation, edgebuffer))
                return false;
            BuildSchedulePart(driver, spaceship, nextTime, startTime, loadingLocation, edgebuffer, planet2Port, output);
            if (output.Count > 0 && output[^1].PlannedDepartureOrArrival!.Value > startTime)
                return false;
            edgebuffer.Clear();
            if (!Dijkstra(graph, loadingLocation, end, edgebuffer))
                return false;
            BuildSchedulePart(driver, spaceship, startTime, endTime, end, edgebuffer, planet2Port, output);
            if (output.Count > 0 && output[^1].PlannedDepartureOrArrival!.Value > endTime)
                return false;
            return true;
        }

        public static (IList<ScheduleElement>? elements, long cost, long time, Driver? driver, Spaceship? spaceship)
            GetBestWay(
            SpacePort loadingPort,
            SpacePort unloadingPort,
            long loadingTime,
            long unloadingTime,
            long volume)
        {
            long currentTime = DateTime.Now.AddDays(1).Ticks;
            if (currentTime > loadingTime || loadingTime > unloadingTime)
                return default;

            var seRepo = RepoKeeper.Instance.ScheduleElementRepo;
            Dictionary<int, SpaceObject> planets = RepoKeeper.Instance.SpaceObjectRepo.GetAll()!.ToDictionary(e => e!.Id)!;
            Dictionary<int, SpacePort> ports = RepoKeeper.Instance.SpacePortRepo.GetAll()!.ToDictionary(e => e!.Id)!;
            Dictionary<int, Edge> edges = RepoKeeper.Instance.EdgeRepo.GetAll()!.ToDictionary(e => e!.Id)!;
            IList<Driver> drivers = RepoKeeper.Instance.DriverRepo.GetAll()!
                .Where(d =>
                {
                    if (d!.CurrentState is null) return false;
                    var current = seRepo.Get(d.CurrentState.Value)!;
                    return current.Order is null && current.IsStop;
                }).ToArray()!;
            IList<Spaceship> spaceships = RepoKeeper.Instance.SpaceshipRepo.GetAll().Where(s =>
            {
                if (s!.Volume < volume || s.CurrentState is null) return false;
                var current = seRepo.Get(s.CurrentState.Value)!;
                return current.Order is null && current.IsStop;
            }).ToList()!;

            Dictionary<int, int> planet2Port = [];
            foreach (var port in ports.Values)
                if (!planet2Port.ContainsKey(port.Planet))
                    planet2Port.Add(port.Planet, port.Id);

            List<ScheduleElement>? result = null;
            long bestCost = 0;
            Driver? bestDriver = null;
            Spaceship? bestSpaceship = null;
            List<ScheduleElement>? current = null;
            foreach (var d in drivers)
                foreach (var s in spaceships)
                {
                    int spaceshipMask = d.SpaceshipClasses & s.Class;
                    if (spaceshipMask != 0)
                    {
                        if (current is null) current = []; else current.Clear();
                        int driverLocation = ports[seRepo.Get(d.CurrentState!.Value)!.DestinationOrStop!.Value].Planet;
                        int spaceshipLocation = ports[seRepo.Get(s.CurrentState!.Value)!.DestinationOrStop!.Value].Planet;
                        if (BuildScheduleElements(
                            d.Id, s.Id,
                            planet2Port, edges,
                            driverLocation, spaceshipLocation,
                            loadingPort.Planet, unloadingPort.Planet,
                            currentTime, loadingTime, unloadingTime,
                            d.QualificationClasses,
                            spaceshipMask,
                            current))
                        {
                            long currentCost = BuildCost(current, d.HiringCost, s.UsageCost);
                            if (result is null || currentCost < bestCost)
                            {
                                bestCost = currentCost;
                                bestDriver = d;
                                bestSpaceship = s;
                                (result, current) = (current, result);
                            }
                        }
                    }
                }
            return (result, bestCost, unloadingTime - currentTime, bestDriver, bestSpaceship);
        }
    }
}
