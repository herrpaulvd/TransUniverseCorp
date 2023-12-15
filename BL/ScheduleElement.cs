using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class ScheduleElement : IBLEntity
{
    [PassSimple]
    public int Id { get; set; }

    [NoPass]
    public long? DepartureOrArrival { get; set; }

    [PassSimple]
    [WithName("DepartureOrArrival")]
    public string SDA
    {
        get => Helper.Time2StringWnull(DepartureOrArrival);
        set => DepartureOrArrival = Helper.String2TimeWnull(value);
    }

    public long PlannedDepartureOrArrival { get; set; }

    [PassSimple]
    [WithName("PlannedDepartureOrArrival")]
    public string SPDA
    {
        get => Helper.Time2String(PlannedDepartureOrArrival);
        set => PlannedDepartureOrArrival = Helper.String2Time(value);
    }

    [PassSimple]
    public int Order { get; set; }

    [NoPass]
    public int? Spaceship { get; set; }

    [WithName("Spaceship")]
    public string Sss
    {
        get => RepoKeeper.Instance.SpaceshipRepo.GetSafely(Spaceship)?.Name ?? "";
        set => Spaceship = RepoKeeper.Instance.SpaceshipRepo.FindByNameSafely(value)?.Id;
    }

    [NoPass]
    public int? Driver { get; set; }

    [WithName("Driver")]
    public string Sd
    {
        get => RepoKeeper.Instance.DriverRepo.GetSafely(Driver)?.Name ?? "";
        set => Driver = RepoKeeper.Instance.DriverRepo.FindByNameSafely(value)?.Id;
    }

    [NoPass]
    public int DestinationOrStop { get; set; }

    [PassSimple]
    [WithName("DestinationOrStop")]
    public string SDO
    {
        get => RepoKeeper.Instance.SpacePortRepo.Get(DestinationOrStop)!.Name;
        set => DestinationOrStop = RepoKeeper.Instance.SpacePortRepo.FindByName(value)!.Id;
    }

    public bool IsStop { get; set; }

    [NoPass]
    public long Time { get; set; }

    [WithName("Time")]
    public string ST
    {
        get => Helper.Time2String(Time);
        set => Time = Helper.String2Time(value);
    }
}
