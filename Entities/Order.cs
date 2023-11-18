using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class Order : IEntity
{
    [PassSimple]
    public int Id { get; set; }

    [NoPass]
    public long LoadingTime { get; set; }

    [NotMapped]
    [WithName("LoadingTime")]
    public string SLT
    {
        get => Helper.Time2String(LoadingTime);
        set => LoadingTime = Helper.String2Time(value);
    }

    [NoPass]
    public int LoadingPort { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("LoadingPort")]
    public string SLP
    {
        get => TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Id == LoadingPort).Name);
        set => LoadingPort = TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Name == value).Id);
    }

    [NoPass]
    public long UnloadingTime { get; set; }

    [NotMapped]
    [WithName("UnloadingTime")]
    public string SUT
    {
        get => Helper.Time2String(UnloadingTime);
        set => UnloadingTime = Helper.String2Time(value);
    }

    [NoPass]
    public int UnloadingPort { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("UnloadingPort")]
    public string SUP
    {
        get => TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Id == UnloadingPort).Name);
        set => UnloadingPort = TransUniverseDbContext.Get(db => db.SpacePorts.First(e => e.Name == value).Id);
    }

    public long Volume { get; set; }

    public long TotalCost { get; set; }

    [NoPass]
    public long TotalTime { get; set; }

    [NotMapped]
    [WithName("TotalTime")]
    public string ST
    {
        get => Helper.Time2String(TotalTime);
        set => TotalTime = Helper.String2Time(value);
    }

    [NoPass]
    public int Spaceship { get; set; }

    [NotMapped]
    [WithName("Spaceship")]
    public string Sss
    {
        get => TransUniverseDbContext.Get(db => db.Spaceships.First(e => e.Id == Spaceship).Name);
        set => Spaceship = TransUniverseDbContext.Get(db => db.Spaceships.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int Driver { get; set; }

    [NotMapped]
    [WithName("Driver")]
    public string Sd
    {
        get => TransUniverseDbContext.Get(db => db.Drivers.First(e => e.Id == Driver).Name);
        set => Driver = TransUniverseDbContext.Get(db => db.Drivers.First(e => e.Name == value).Id);
    }

    public int CurrentState { get; set; }

    public virtual ScheduleElement CurrentStateNavigation { get; set; } = null!;

    public virtual Driver DriverNavigation { get; set; } = null!;

    public virtual SpacePort LoadingPortNavigation { get; set; } = null!;

    public virtual ICollection<ScheduleElement> ScheduleElements { get; set; } = new List<ScheduleElement>();

    public virtual Spaceship SpaceshipNavigation { get; set; } = null!;

    public virtual SpacePort UnloadingPortNavigation { get; set; } = null!;
}
