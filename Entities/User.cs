using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class User : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [PassSimple]
    public string Login { get; set; } = null!;

    public long PasswordHash { get; set; }

    [NoPass]
    public int? Customer { get; set; }

    [NotMapped][WithName("Customer")]
    public string Sc
    {
        get => TransUniverseDbContext.Get(db => db.Customers.FirstOrDefault(e => e.Id == Customer)?.Name ?? "");
        set => Customer = value.Length == 0 ? null : TransUniverseDbContext.Get(db => db.Customers.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int? Driver { get; set; }

    [NotMapped][WithName("Driver")]
    public string Sd
    {
        get => TransUniverseDbContext.Get(db => db.Drivers.FirstOrDefault(e => e.Id == Driver)?.Name ?? "");
        set => Driver = value.Length == 0 ? null : TransUniverseDbContext.Get(db => db.Drivers.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int Roles { get; set; }

    [PassSimple]
    [NotMapped][WithName("Roles")]
    public string SR
    {
        get => Helper.Int2Classes(Roles);
        set => Roles = Helper.Classes2Int(value);
    }

    public virtual Customer? CustomerNavigation { get; set; }

    public virtual Driver? DriverNavigation { get; set; }
}
