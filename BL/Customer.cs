using System;
using System.Collections.Generic;
using UnviersalMV;

namespace BL;

public partial class Customer : INamedBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    [PassSimple]
    public string Email { get; set; } = null!;

    public bool Corporative { get; set; }

    public bool CheckConsistency() => true;

    public bool CheckConsistencyOnDelete() =>
        Id.CheckOrderOnDelete(o => o.Customer)
        && Id.CheckUserOnDelete(u => u.Customer);
}
