using System;
using System.Collections.Generic;

namespace SpaceRouteDAL;

public partial class SpaceObject : Entities.INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Kind { get; set; }

    public int? SystemCenter { get; set; }

    public int SystemPosition { get; set; }
}
