using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class Driver : INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int QualificationClasses { get; set; }

    public int SpaceshipClasses { get; set; }

    public long HiringCost { get; set; }

    public int? CurrentState { get; set; }

    public virtual ScheduleElement? CurrentStateNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
