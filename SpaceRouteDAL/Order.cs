using System;
using System.Collections.Generic;

namespace SpaceRouteDAL;

public partial class Order : Entities.IDALEntity
{
    public int Id { get; set; }

    public long LoadingTime { get; set; }

    public int LoadingPort { get; set; }

    public long UnloadingTime { get; set; }

    public int UnloadingPort { get; set; }

    public long Volume { get; set; }

    public long TotalCost { get; set; }

    public long TotalTime { get; set; }

    public int Spaceship { get; set; }

    public int Driver { get; set; }

    public int Customer { get; set; }

    public int CurrentState { get; set; }

    public int Status { get; set; }
}
