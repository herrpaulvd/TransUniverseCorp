using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class Edge : IBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [NoPass]
    public int Start { get; set; }

    [PassSimple]
    [WithName("Start")]
    public string SStart
    {
        get => RepoKeeper.Instance.SpaceObjectRepo.Get(Start)!.Name;
        set => Start = RepoKeeper.Instance.SpaceObjectRepo.FindByName(value)!.Id;
    }

    [NoPass]
    public int End { get; set; }

    [PassSimple]
    [WithName("End")]
    public string SEnd
    {
        get => RepoKeeper.Instance.SpaceObjectRepo.Get(End)!.Name;
        set => End = RepoKeeper.Instance.SpaceObjectRepo.FindByName(value)!.Id;
    }

    [NoPass]
    public long Time { get; set; }

    [WithName("Time")]
    public string ST
    {
        get => Helper.TimeSpan2String(Time);
        set => Time = Helper.String2TimeSpan(value);
    }

    [NoPass]
    public int SpaceshipClasses { get; set; }

    [NoPass]
    public int QualificationClasses { get; set; }

    [WithName("SpaceshipClasses")]
    public string Ssc
    {
        get => Helper.Int2Classes(SpaceshipClasses);
        set => SpaceshipClasses = Helper.Classes2Int(value);
    }

    [WithName("QualificationClasses")]
    public string Sqc
    {
        get => Helper.Int2Classes(QualificationClasses);
        set => QualificationClasses = Helper.Classes2Int(value);
    }

    public bool CheckConsistency() => Start.CheckSpaceObject() && End.CheckSpaceObject();

    public bool CheckConsistencyOnDelete() => true;
}
