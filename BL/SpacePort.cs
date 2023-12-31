using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class SpacePort : INamedBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [NoPass]
    public int Planet { get; set; }

    [PassSimple]
    [WithName("Planet")]
    public string Sp
    {
        get => RepoKeeper.Instance.SpaceObjectRepo.Get(Planet)!.Name;
        set => Planet = RepoKeeper.Instance.SpaceObjectRepo.FindByName(value)!.Id;
    }

    public double Longtitude { get; set; }

    public double Latitude { get; set; }

    public double Altitude { get; set; }

    public bool CheckConsistency() => Planet.CheckSpaceObject();

    public bool CheckConsistencyOnDelete() =>
        Id.CheckOrderOnDelete(o => o.LoadingPort)
        && Id.CheckOrderOnDelete(o => o.UnloadingPort)
        && Id.CheckScheduleElementOnDelete(se => se.DestinationOrStop);
}
