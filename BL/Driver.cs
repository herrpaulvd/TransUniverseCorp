﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class Driver : INamedBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    [PassSimple]
    public string Email { get; set; } = null!;

    [NoPass]
    public int QualificationClasses { get; set; }

    [WithName("QualificationClasses")]
    public string Sqc
    {
        get => Helper.Int2Classes(QualificationClasses);
        set => QualificationClasses = Helper.Classes2Int(value);
    }

    [NoPass]
    public int SpaceshipClasses { get; set; }

    [WithName("SpaceshipClasses")]
    public string Ssc
    {
        get => Helper.Int2Classes(SpaceshipClasses);
        set => SpaceshipClasses = Helper.Classes2Int(value);
    }

    public long HiringCost { get; set; }

    [NoPass]
    public int? CurrentState { get; set; }

    [WithName("CurrentState")]
    public int CS
    {
        get => Helper.State2Int(CurrentState);
        set => CurrentState = Helper.Int2State(value);
    }

    public bool CheckConsistency() => CurrentState.CheckScheduleElement();

    public bool CheckConsistencyOnDelete() =>
        Id.CheckOrderOnDelete(o => o.Driver)
        && Id.CheckScheduleElementOnDelete(o => o.Driver)
        && Id.CheckUserOnDelete(o => o.Driver);
}
