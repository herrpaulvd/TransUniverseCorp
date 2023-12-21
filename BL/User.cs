using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class User : IBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [PassSimple]
    public string Login { get; set; } = null!;

    public long PasswordHash { get; set; }

    [NoPass]
    public int? Customer { get; set; }

    [WithName("Customer")]
    public string CustomerName
    {
        get => RepoKeeper.Instance.CustomerRepo.GetSafely(Customer)?.Name ?? "";
        set => Customer = RepoKeeper.Instance.CustomerRepo.FindByNameSafely(value)?.Id;
    }

    [NoPass]
    public int? Driver { get; set; }

    [WithName("Driver")]
    public string DriverName
    {
        get => RepoKeeper.Instance.DriverRepo.GetSafely(Driver)?.Name ?? "";
        set => Driver = RepoKeeper.Instance.DriverRepo.FindByNameSafely(value)?.Id;
    }

    [NoPass]
    public int Roles { get; set; }

    [PassSimple]
    [WithName("Roles")]
    public string RolesAsString
    {
        get => Helper.Int2Classes(Roles);
        set => Roles = Helper.Classes2Int(value);
    }
}
