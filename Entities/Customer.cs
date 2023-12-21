using System;
using System.Collections.Generic;

namespace Entities;

public partial class Customer : INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Corporative { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
