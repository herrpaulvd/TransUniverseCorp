using System;
using System.Collections.Generic;

namespace SpaceRouteDAL;

public partial class Edge : Entities.IDALEntity
{
    public int Id { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public long Time { get; set; }

    public int SpaceshipClasses { get; set; }

    public int QualificationClasses { get; set; }
}
