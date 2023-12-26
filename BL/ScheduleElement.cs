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

    public long? PlannedDepartureOrArrival { get; set; }

    [PassSimple]
    [WithName("PlannedDepartureOrArrival")]
    public string ShowPlannedDepartureOrArrival
    {
        get => Helper.Time2StringWnull(PlannedDepartureOrArrival);
        set => PlannedDepartureOrArrival = Helper.String2TimeWnull(value);
    }

    [NoPass]
    public int? Order { get; set; }

    [PassSimple]
    [WithName("Order")]
    public int ShowOrder
    {
        get => Helper.State2Int(Order);
        set => Order = Helper.Int2State(value);
    }

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
    public int? DestinationOrStop { get; set; }

    [PassSimple]
    [WithName("DestinationOrStop")]
    public string DestinationOrStopPortName
    {
        get => RepoKeeper.Instance.SpacePortRepo.GetSafely(DestinationOrStop)?.Name ?? "";
        set => DestinationOrStop = RepoKeeper.Instance.SpacePortRepo.FindByNameSafely(value)?.Id;
    }

    public bool IsStop { get; set; }

    [NoPass]
    public long Time { get; set; }

    public int? Next { get; set; }

    [WithName("Next")]
    public int ShowNext
    {
        get => Helper.State2Int(Next);
        set => Next = Helper.Int2State(value);
    }

    //[WithName("Time")]
    //public string ST
    //{
    //    get => Helper.Time2String(Time);
    //    set => Time = Helper.String2Time(value);
    //}
}
