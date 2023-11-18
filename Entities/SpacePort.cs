using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class SpacePort : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [NoPass]
    public int Planet { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("Planet")]
    public string Sp
    {
        get => TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Id == Planet).Name);
        set => Planet = TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Name == value).Id);
    }

    public double Longtitude { get; set; }

    public double Latitude { get; set; }

    public double Altitude { get; set; }

    public virtual ICollection<Order> OrderLoadingPortNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUnloadingPortNavigations { get; set; } = new List<Order>();

    public virtual SpaceObject PlanetNavigation { get; set; } = null!;

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();
}
