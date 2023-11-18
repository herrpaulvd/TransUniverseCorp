using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class Driver : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    [PassSimple]
    public string Email { get; set; } = null!;

    [NoPass]
    public int QualificationClasses { get; set; }

    [NotMapped][WithName("QualificationClasses")]
    public string Sqc
    {
        get => Helper.Int2Classes(QualificationClasses);
        set => QualificationClasses = Helper.Classes2Int(value);
    }

    [NoPass]
    public int SpaceshipClasses { get; set; }

    [NotMapped][WithName("SpaceshipClasses")]
    public string Ssc
    {
        get => Helper.Int2Classes(SpaceshipClasses);
        set => SpaceshipClasses = Helper.Classes2Int(value);
    }

    public long HiringCost { get; set; }

    [NoPass]
    public int? CurrentState { get; set; }

    [NotMapped][WithName("CurrentState")]
    public int CS
    {
        get => Helper.State2Int(CurrentState);
        set => CurrentState = Helper.Int2State(value);
    }

    public virtual ScheduleElement? CurrentStateNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
