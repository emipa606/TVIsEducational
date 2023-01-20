using System;
using System.Linq;
using RimWorld;
using Verse;

namespace TVIsEducational;

public class CompTVIsEducational : ThingComp
{
    public CompProperties_TVIsEducational Props => (CompProperties_TVIsEducational)props;

    public Building TV => parent as Building;


    public override void CompTick()
    {
        var tickPeriod = 2500;
        base.CompTick();
        if (Find.TickManager.TicksGame % tickPeriod != 0 || !isFunctional(TV))
        {
            return;
        }

        if (!ModsConfig.BiotechActive)
        {
            return;
        }

        var list = GenRadial.RadialCellsAround(TV.Position, Math.Min(15, Props.effectRadius), true).ToList();
        if (list.Count <= 0)
        {
            return;
        }

        foreach (var cell in list)
        {
            if (!IsValidCell(cell, TV))
            {
                continue;
            }

            var things = cell.GetThingList(TV.Map);
            if (things.Count <= 0)
            {
                continue;
            }

            foreach (var thing in things)
            {
                if (thing is not Pawn pawn)
                {
                    continue;
                }

                if (!IsValidPawn(pawn))
                {
                    continue;
                }

                var factor = 1f;
                var needs = pawn.needs;

                if (needs?.mood != null)
                {
                    factor *= pawn.needs.mood.CurLevel * Math.Min(1f, Props.effectFactor);
                }

                factor *= pawn.health.capacities.GetLevel(PawnCapacityDefOf.Sight);
                factor *= pawn.health.capacities.GetLevel(PawnCapacityDefOf
                    .Hearing);
                var tech = TechLevel.Industrial;
                if (pawn.Faction != null)
                {
                    tech = pawn.Faction.def.techLevel;
                }

                switch (tech)
                {
                    case TechLevel.Animal:
                        factor *= 0.7f;
                        break;
                    case TechLevel.Neolithic:
                        factor *= 0.75f;
                        break;
                    case TechLevel.Medieval:
                        factor *= 0.8f;
                        break;
                    case TechLevel.Spacer:
                        factor *= 0.85f;
                        break;
                    case TechLevel.Ultra:
                        factor *= 0.9f;
                        break;
                    case TechLevel.Archotech:
                        factor *= 0.95f;
                        break;
                    default:
                        factor *= 0.8f;
                        break;
                }

                if (!(Math.Min(1f, factor) > 0f))
                {
                    Log.Message($"Too small factor {factor} for {pawn}");
                    continue;
                }

                var value = 0.015f * LearningUtility.LearningRateFactor(pawn) * factor;
                Log.Message($"Giving {pawn} learning: {value}");

                pawn.needs.learning.Learn(value);

                GiveTVThought(pawn);
            }
        }
    }

    public void GiveTVThought(Pawn pawn)
    {
        if (pawn == null)
        {
            return;
        }

        var needs = pawn.needs;
        var hasNeed = needs?.mood != null;

        if (!hasNeed)
        {
            return;
        }

        var tvdef = DefDatabase<ThoughtDef>.GetNamed("TVIsEducational", false);
        if (tvdef != null)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(tvdef);
        }
    }

    public bool IsValidPawn(Pawn pawn)
    {
        return pawn.DevelopmentalStage.Child() && pawn.learning != null && pawn.Awake() && !pawn.Dead &&
               !pawn.IsBurning() && !pawn.InMentalState;
    }

    public bool IsValidCell(IntVec3 cell, Building tv)
    {
        return cell.IsValid && cell.InBounds(tv.Map);
    }


    public bool isFunctional(Building tv)
    {
        return !tv.DestroyedOrNull() && tv is { Map: { }, Spawned: true } &&
               tv.TryGetComp<CompPowerTrader>().PowerOn && !tv.IsBrokenDown();
    }
}