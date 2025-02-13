namespace SaberRandomizer.Utilities;

internal static class TrailUtils
{
    // Legacy trail duration in frames
    public const int LegacyDuration = 30;

    // Default duration of the saber trail in seconds
    public const float DefaultDuration = 0.4f;

    /// <summary>
    /// Converts legacy trail length in frames to trail duration in seconds used by <see cref="SaberTrail"/>
    /// </summary>
    /// <param name="customTrailLength">Trail length from a CustomTrail, in frames</param>
    /// <returns>Trail duration in seconds</returns>
    public static float ConvertLegacyLength(int customTrailLength) =>
        customTrailLength / (float)LegacyDuration * DefaultDuration;
}