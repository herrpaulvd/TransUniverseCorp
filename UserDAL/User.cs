using System;
using System.Collections.Generic;

namespace UserDAL;

public partial class User : Entities.IDALEntity
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public long PasswordHash { get; set; }

    public int? Customer { get; set; }

    public int? Driver { get; set; }

    public int Roles { get; set; }
}
