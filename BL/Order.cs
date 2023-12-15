﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnviersalMV;

namespace BL;

public partial class Order : IBLEntity
{
    [PassSimple]
    public int Id { get; set; }

    [NoPass]
    public long LoadingTime { get; set; }

    [WithName("LoadingTime")]
    public string SLT
    {
        get => Helper.Time2String(LoadingTime);
        set => LoadingTime = Helper.String2Time(value);
    }

    [NoPass]
    public int LoadingPort { get; set; }

    [PassSimple]
    [WithName("LoadingPort")]
    public string SLP
    {
        get => RepoKeeper.Instance.SpacePortRepo.Get(LoadingPort)!.Name;
        set => LoadingPort = RepoKeeper.Instance.SpacePortRepo.FindByName(value)!.Id;
    }

    [NoPass]
    public long UnloadingTime { get; set; }

    [WithName("UnloadingTime")]
    public string SUT
    {
        get => Helper.Time2String(UnloadingTime);
        set => UnloadingTime = Helper.String2Time(value);
    }

    [NoPass]
    public int UnloadingPort { get; set; }

    [PassSimple]
    [WithName("UnloadingPort")]
    public string SUP
    {
        get => RepoKeeper.Instance.SpacePortRepo.Get(UnloadingPort)!.Name;
        set => UnloadingPort = RepoKeeper.Instance.SpacePortRepo.FindByName(value)!.Id;
    }

    public long Volume { get; set; }

    public long TotalCost { get; set; }

    [NoPass]
    public long TotalTime { get; set; }

    [WithName("TotalTime")]
    public string ST
    {
        get => Helper.Time2String(TotalTime);
        set => TotalTime = Helper.String2Time(value);
    }

    [NoPass]
    public int Spaceship { get; set; }

    [WithName("Spaceship")]
    public string Sss
    {
        get => RepoKeeper.Instance.SpaceshipRepo.Get(Spaceship)!.Name;
        set => Spaceship = RepoKeeper.Instance.SpaceshipRepo.FindByName(value)!.Id;
    }

    [NoPass]
    public int Driver { get; set; }

    [WithName("Driver")]
    public string Sd
    {
        get => RepoKeeper.Instance.DriverRepo.Get(Driver)!.Name;
        set => Driver = RepoKeeper.Instance.DriverRepo.FindByName(value)!.Id;
    }

    public int CurrentState { get; set; }
}