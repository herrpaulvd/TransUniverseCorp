using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class SpacePort : INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Planet { get; set; }

    public double Longtitude { get; set; }

    public double Latitude { get; set; }

    public double Altitude { get; set; }

    public virtual ICollection<Order> OrderLoadingPortNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUnloadingPortNavigations { get; set; } = new List<Order>();

    public virtual SpaceObject PlanetNavigation { get; set; } = null!;

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();
}
