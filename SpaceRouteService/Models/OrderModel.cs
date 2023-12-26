using BL;

namespace SpaceRouteService.Models
{
    public class OrderModel
    {
        public IList<ScheduleElement> ScheduleElements { get; set; }
        public long Cost { get; set; }
        public Driver Driver { get; set; }
        public Spaceship Spaceship { get; set; }
        public SpacePort LoadingPort { get; set; }
        public SpacePort UnloadingPort { get; set; }
        public int Index { get; set; }
        public long LoadingTime { get; set; }
        public long UnloadingTime { get; set; }
        public long TotalTime { get; set; }
        public long Volume { get; set; }
        public int Customer { get; set; }
    }
}
