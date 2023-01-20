using Verse;

namespace TVIsEducational;

public class CompProperties_TVIsEducational : CompProperties
{
    public float effectFactor;

    public int effectRadius;

    public CompProperties_TVIsEducational()
    {
        compClass = typeof(CompTVIsEducational);
    }
}