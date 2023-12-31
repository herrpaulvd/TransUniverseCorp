using System;
using System.Collections.Generic;

namespace CustomerDAL;

public partial class Customer : Entities.INamedDALEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Corporative { get; set; }
}
