using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class Edge : IDALEntity
{
    public int Id { get; set; }

    public int Start { get; set; }

    public int End { get; set; }

    public long Time { get; set; }

    public int SpaceshipClasses { get; set; }

    public int QualificationClasses { get; set; }

    public virtual SpaceObject EndNavigation { get; set; } = null!;

    public virtual SpaceObject StartNavigation { get; set; } = null!;
}
