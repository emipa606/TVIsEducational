using Verse;

namespace TVIsEducational;

public class GameComponent_TVTimeTracker : GameComponent
{
    public int seenTvTicks;

    public GameComponent_TVTimeTracker(Game game)
    {
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref seenTvTicks, "seenTvTicks");
    }
}