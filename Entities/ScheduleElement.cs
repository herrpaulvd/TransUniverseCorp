using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class ScheduleElement : IDALEntity
{
    public int Id { get; set; }

    public long? DepartureOrArrival { get; set; }

    public long PlannedDepartureOrArrival { get; set; }

    public int Order { get; set; }

    public int? Spaceship { get; set; }

    public int? Driver { get; set; }

    public int DestinationOrStop { get; set; }

    public bool IsStop { get; set; }

    public long Time { get; set; }

    public virtual SpacePort DestinationOrStopNavigation { get; set; } = null!;

    public virtual Driver? DriverNavigation { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Spaceship? SpaceshipNavigation { get; set; }

    public virtual ICollection<Spaceship> Spaceships { get; set; } = new List<Spaceship>();
}
