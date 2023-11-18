using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class Spaceship : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    [PassSimple]
    public string Model { get; set; } = null!;

    [NoPass]
    public int Class { get; set; }

    [PassSimple]
    [NotMapped][WithName("Class")]
    public string Ssc
    {
        get => Helper.Int2Classes(Class);
        set => Class = Helper.Classes2Int(value);
    }

    public long UsageCost { get; set; }

    public long Volume { get; set; }

    [NoPass]
    public int? CurrentState { get; set; }

    [NotMapped][WithName("CurrentState")]
    public int CS
    {
        get => Helper.State2Int(CurrentState);
        set => CurrentState = Helper.Int2State(value);
    }

    public virtual ScheduleElement CurrentStateNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();
}
