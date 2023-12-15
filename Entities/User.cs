using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public partial class User : IDALEntity
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public long PasswordHash { get; set; }

    public int? Customer { get; set; }

    public int? Driver { get; set; }

    public int Roles { get; set; }

    public virtual Customer? CustomerNavigation { get; set; }

    public virtual Driver? DriverNavigation { get; set; }
}
