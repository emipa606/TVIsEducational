using System;
using System.Linq;
using RimWorld;
using Verse;

namespace TVIsEducational;

public class CompTVIsEducational : ThingComp
{
    private const int TickPeriod = 2500;
    private CompProperties_TVIsEducational Props => (CompProperties_TVIsEducational)props;

    private Building TV => parent as Building;

    public override void CompTick()
    {
        base.CompTick();
        if (Find.TickManager.TicksGame % TickPeriod != 0 || !isFunctional(TV))
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
            if (!isValidCell(cell, TV))
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

                if (!isValidPawn(pawn))
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
                    continue;
                }

                var value = 0.015f * LearningUtility.LearningRateFactor(pawn) * factor;

                pawn.needs.learning.Learn(value);

                giveTVThought(pawn);
            }
        }
    }

    private static void giveTVThought(Pawn pawn)
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

        var tvDef = DefDatabase<ThoughtDef>.GetNamed("TVIsEducational", false);
        if (tvDef == null)
        {
            return;
        }

        pawn.needs.mood.thoughts.memories.TryGainMemory(tvDef);
        Current.Game.GetComponent<GameComponent_TVTimeTracker>().SeenTvTicks += TickPeriod;
    }

    private static bool isValidPawn(Pawn pawn)
    {
        return pawn.DevelopmentalStage.Child() && pawn.learning != null && pawn.Awake() && !pawn.Dead &&
               !pawn.IsBurning() && !pawn.InMentalState && pawn.GetPosture().Laying();
    }

    private static bool isValidCell(IntVec3 cell, Building tv)
    {
        return cell.IsValid && cell.InBounds(tv.Map);
    }


    private static bool isFunctional(Building tv)
    {
        return !tv.DestroyedOrNull() && tv is { Map: not null, Spawned: true } &&
               tv.TryGetComp<CompPowerTrader>().PowerOn && !tv.IsBrokenDown();
    }
}