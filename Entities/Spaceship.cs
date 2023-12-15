using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class Spaceship : INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Class { get; set; }

    public long UsageCost { get; set; }

    public long Volume { get; set; }

    public int? CurrentState { get; set; }

    public virtual ScheduleElement CurrentStateNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();
}
