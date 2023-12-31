using System;
using System.Collections.Generic;

namespace SpaceRouteDAL;

public partial class SpacePort : Entities.INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Planet { get; set; }

    public double Longtitude { get; set; }

    public double Latitude { get; set; }

    public double Altitude { get; set; }
}
