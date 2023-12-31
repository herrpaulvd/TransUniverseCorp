using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class SpaceObject : INamedBLEntity
{
    [Hidden]
    public int Id { get; set; }

    [PassSimple]
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    [NoPass]
    public int Kind { get; set; }

    [PassSimple]
    [WithName("Kind")]
    public string SK
    {
        get => Kind switch
        {
            0 => "Planet",
            1 => "Star",
            2 => "Galaxy",
            _ => "Unknown"
        };
        set => Kind = value switch
        {
            "Planet" => 0,
            "Star" => 1,
            "Galaxy" => 2,
            _ => 3
        };
    }

    [NoPass]
    public int? SystemCenter { get; set; }

    [WithName("SystemCenter")]
    public string Sd
    {
        get => RepoKeeper.Instance.SpaceObjectRepo.GetSafely(SystemCenter)?.Name ?? "";
        set => SystemCenter = RepoKeeper.Instance.SpaceObjectRepo.FindByNameSafely(value)?.Id;
    }

    public int SystemPosition { get; set; }

    public bool CheckConsistency() => SystemCenter.CheckSpaceObject();

    public bool CheckConsistencyOnDelete() =>
        Id.CheckEdgeOnDelete(e => e.Start)
        && Id.CheckEdgeOnDelete(e => e.End)
        && Id.CheckSpaceObjectOnDelete(so => so.SystemCenter)
        && Id.CheckSpacePortOnDelete(p => p.Planet);

}
