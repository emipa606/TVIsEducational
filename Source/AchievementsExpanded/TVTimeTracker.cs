﻿using System;
using AchievementsExpanded;
using RimWorld;
using Verse;

namespace TVIsEducational;

public class TVTimeTracker : TrackerBase
{
    private int count = 1;

    [Unsaved] private int triggeredTicks; //Only for display

    public TVTimeTracker()
    {
    }

    public TVTimeTracker(TVTimeTracker reference) : base(reference)
    {
        count = reference.count;
    }

    public override string Key
    {
        get => "TVTimeCurrentTracker";
        set { }
    }

    public override Func<bool> AttachToLongTick => Trigger;

    protected override string[] DebugText => [$"Count: {count}"];

    public override (float percent, string text) PercentComplete => count > 1
        ? ((float)triggeredTicks / GenDate.TicksPerHour / count,
            $"{Math.Round((float)triggeredTicks / GenDate.TicksPerHour)} / {count}")
        : base.PercentComplete;


    public override bool UnlockOnStartup => Trigger();


    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref count, "count", 1);
    }

    public override bool Trigger()
    {
        base.Trigger();
        triggeredTicks = Current.Game.GetComponent<GameComponent_TVTimeTracker>().SeenTvTicks;

        return triggeredTicks / GenDate.TicksPerHour >= count;
    }
}