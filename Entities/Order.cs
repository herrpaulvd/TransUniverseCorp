using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class Order : IDALEntity
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

    public int CurrentState { get; set; }

    public virtual ScheduleElement CurrentStateNavigation { get; set; } = null!;

    public virtual Driver DriverNavigation { get; set; } = null!;

    public virtual SpacePort LoadingPortNavigation { get; set; } = null!;

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();

    public virtual Spaceship SpaceshipNavigation { get; set; } = null!;

    public virtual SpacePort UnloadingPortNavigation { get; set; } = null!;
}
