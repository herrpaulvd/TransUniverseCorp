using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class ScheduleElement : IEntity
{
    [PassSimple]
    public int Id { get; set; }

    [NoPass]
    public long DepartureOrArrival { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("DepartureOrArrival")]
    public string SDA
    {
        get => Helper.Time2String(DepartureOrArrival);
        set => DepartureOrArrival = Helper.String2Time(value);
    }

    public long? PlannedDepartureOrArrival { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("PlannedDepartureOrArrival")]
    public string SPDA
    {
        get => Helper.Time2StringWnull(PlannedDepartureOrArrival);
        set => PlannedDepartureOrArrival = Helper.String2TimeWnull(value);
    }

    [PassSimple]
    public int Order { get; set; }

    [NoPass]
    public int? Spaceship { get; set; }

    [NotMapped]
    [WithName("Spaceship")]
    public string Sss
    {
        get => TransUniverseDbContext.Get(db => db.Spaceships.FirstOrDefault(e => e.Id == Spaceship)?.Name ?? "");
        set => Spaceship = value.Length == 0 ? null : TransUniverseDbContext.Get(db => db.Spaceships.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int? Driver { get; set; }

    [NotMapped]
    [WithName("Driver")]
    public string Sd
    {
        get => TransUniverseDbContext.Get(db => db.Drivers.FirstOrDefault(e => e.Id == Driver)?.Name ?? "");
        set => Driver = value.Length == 0 ? null : TransUniverseDbContext.Get(db => db.Drivers.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int DestinationOrStop { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("DestinationOrStop")]
    public string SDO
    {
        get => TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Id == DestinationOrStop).Name);
        set => DestinationOrStop = TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Name == value).Id);
    }

    public bool IsStop { get; set; }

    [NoPass]
    public long Time { get; set; }

    [NotMapped]
    [WithName("Time")]
    public string ST
    {
        get => Helper.Time2String(Time);
        set => Time = Helper.String2Time(value);
    }

    public virtual SpacePort DestinationOrStopNavigation { get; set; } = null!;

    public virtual Driver? DriverNavigation { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual Order OrderNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Spaceship? SpaceshipNavigation { get; set; }

    public virtual ICollection<Spaceship> Spaceships { get; set; } = new List<Spaceship>();
}
