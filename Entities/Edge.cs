using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace Entities;

public partial class Edge : IEntity
{
    [NoPass]
    public int Id { get; set; }

    [NoPass]
    public int Start { get; set; }

    [PassSimple]
    [NotMapped][WithName("Start")]
    public string SStart
    {
        get => TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Id == Start).Name);
        set => Start = TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Name == value).Id);
    }

    [NoPass]
    public int End { get; set; }

    [PassSimple]
    [NotMapped]
    [WithName("End")]
    public string SEnd
    {
        get => TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Id == End).Name);
        set => End = TransUniverseDbContext.Get(db => db.SpaceObjects.First(e => e.Name == value).Id);
    }

    [NoPass]
    public long Time { get; set; }

    [NotMapped]
    [WithName("Time")]
    public string ST
    {
        get => Helper.Time2String(Time);
        set => Time = Helper.String2Time(value);
    }

    [NoPass]
    public int SpaceshipClasses { get; set; }

    [NoPass]
    public int QualificationClasses { get; set; }

    [NotMapped]
    [WithName("SpaceshipClasses")]
    public string Ssc
    {
        get => Helper.Int2Classes(SpaceshipClasses);
        set => SpaceshipClasses = Helper.Classes2Int(value);
    }

    [NotMapped]
    [WithName("QualificationClasses")]
    public string Sqc
    {
        get => Helper.Int2Classes(QualificationClasses);
        set => QualificationClasses = Helper.Classes2Int(value);
    }

    public virtual SpaceObject EndNavigation { get; set; } = null!;

    public virtual SpaceObject StartNavigation { get; set; } = null!;
}
