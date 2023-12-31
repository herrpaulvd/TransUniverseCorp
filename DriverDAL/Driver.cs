using System;
using System.Collections.Generic;

namespace DriverDAL;

public partial class Driver : Entities.INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int QualificationClasses { get; set; }

    public int SpaceshipClasses { get; set; }

    public long HiringCost { get; set; }

    public int? CurrentState { get; set; }
}
