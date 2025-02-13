using UnityEngine;

namespace SaberRandomizer.Models;

internal interface ITrailData
{
    public Material? Material { get; }
    public float LengthSeconds { get; }
    
    public bool UseCustomColor { get; }
    public Color CustomColor { get; }
    public Color ColorMultiplier { get; }

    public Vector3 TrailTopOffset { get; }
    public Vector3 TrailBottomOffset { get; }
}