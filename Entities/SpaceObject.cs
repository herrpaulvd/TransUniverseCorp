using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class SpaceObject : INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Kind { get; set; }

    public int? SystemCenter { get; set; }

    public int SystemPosition { get; set; }

    public virtual ICollection<Edge> EdgeEndNavigations { get; set; } = new List<Edge>();

    public virtual ICollection<Edge> EdgeStartNavigations { get; set; } = new List<Edge>();

    public virtual ICollection<SpaceObject> InverseSystemCenterNavigation { get; set; } = new List<SpaceObject>();

    public virtual ICollection<SpacePort> SpacePorts { get; set; } = new List<SpacePort>();

    public virtual SpaceObject? SystemCenterNavigation { get; set; }
}
