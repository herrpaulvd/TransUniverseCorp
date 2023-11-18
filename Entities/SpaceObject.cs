using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class SpaceObject : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [NoPass]
    public int Kind { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("Kind")]
    public string SK
    {
        get => Kind switch
        {
            0 => "Planet",
            1 => "Star",
            2 => "Galaxy",
            _ => "Unknown"
        };
        set => Kind = value switch
        {
            "Planet" => 0,
            "Star" => 1,
            "Galaxy" => 2,
            _ => 3
        };
    }

    [NoPass]
    public int? SystemCenter { get; set; }

    [NotMapped]
    [WithName("SystemCenter")]
    public string Sd
    {
        get => TransUniverseDbContext.Get(db => db.SpaceObjects.FirstOrDefault(e => e.Id == SystemCenter)?.Name ?? "");
        set => SystemCenter = value.Length == 0 ? null : TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Name == value).Id);
    }

    public int SystemPosition { get; set; }

    public virtual ICollection<Edge> EdgeEndNavigations { get; set; } = new List<Edge>();

    public virtual ICollection<Edge> EdgeStartNavigations { get; set; } = new List<Edge>();

    public virtual ICollection<SpaceObject> InverseSystemCenterNavigation { get; set; } = new List<SpaceObject>();

    public virtual ICollection<SpacePort> SpacePorts { get; set; } = new List<SpacePort>();

    public virtual SpaceObject? SystemCenterNavigation { get; set; }
}
