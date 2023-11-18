using System;
using System.Collections.Generic;
using UnviersalMV;

namespace Entities;

public partial class Customer : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    [PassSimple]
    public string Email { get; set; } = null!;

    public bool Corporative { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
