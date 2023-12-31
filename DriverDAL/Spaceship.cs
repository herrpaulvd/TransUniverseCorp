using System;
using System.Collections.Generic;

namespace DriverDAL;

public partial class Spaceship : Entities.INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Class { get; set; }

    public long UsageCost { get; set; }

    public long Volume { get; set; }

    public int? CurrentState { get; set; }
}
