using UnityEngine;

namespace SaberRandomizer.Models;

internal class SaberInstanceSet
{
    private readonly GameObject leftSaber;
    private readonly GameObject rightSaber;
    private readonly ITrailData[] leftTrails;
    private readonly ITrailData[] rightTrails;

    public SaberInstanceSet(
        GameObject leftSaber,
        GameObject rightSaber,
        ITrailData[] leftTrails,
        ITrailData[] rightTrails)
    {
        this.leftSaber = leftSaber;
        this.rightSaber = rightSaber;
        this.leftTrails = leftTrails;
        this.rightTrails = rightTrails;
    }
    
    public GameObject SaberForType(SaberType saberType) => saberType == SaberType.SaberA ? leftSaber : rightSaber;
    public ITrailData[] TrailForType(SaberType saberType) => saberType == SaberType.SaberA ? leftTrails : rightTrails;
}