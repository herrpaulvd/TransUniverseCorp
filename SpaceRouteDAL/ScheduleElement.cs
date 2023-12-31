using System;
using System.Collections.Generic;

namespace SpaceRouteDAL;

public partial class ScheduleElement : Entities.IDALEntity
{
    public int Id { get; set; }

    public long? DepartureOrArrival { get; set; }

    public long? PlannedDepartureOrArrival { get; set; }

    public int? Order { get; set; }

    public int? Spaceship { get; set; }

    public int? Driver { get; set; }

    public int? DestinationOrStop { get; set; }

    public bool IsStop { get; set; }

    public long Time { get; set; }

    public int? Next { get; set; }
}
