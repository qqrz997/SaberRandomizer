using SaberRandomizer.Utilities;
using UnityEngine;

namespace SaberRandomizer.Game;

internal class DefaultSaberColorer : MonoBehaviour
{
    private SetSaberGlowColor[] setSaberGlowColors = null!;
    private SetSaberFakeGlowColor[] setSaberFakeGlowColors = null!;

    private void Awake()
    {
        setSaberGlowColors = GetComponentsInChildren<SetSaberGlowColor>();
        setSaberFakeGlowColors = GetComponentsInChildren<SetSaberFakeGlowColor>();
    }

    public void SetColor(Color color)
    {
        foreach (var setSaberGlowColor in setSaberGlowColors)
        {
            setSaberGlowColor.SetNewColor(color);
        }

        foreach (var setSaberFakeGlowColor in setSaberFakeGlowColors)
        {
            setSaberFakeGlowColor.SetNewColor(color);
        }
    }
}

internal static class DefaultSaberColoring
{
    public static void SetNewColor(this SetSaberGlowColor setSaberGlowColor, Color color)
    {
        var materialPropertyBlock = setSaberGlowColor._materialPropertyBlock ?? new MaterialPropertyBlock();

        setSaberGlowColor._propertyTintColorPairs.ForEach(pair =>
            materialPropertyBlock.SetColor(pair.property, color * pair.tintColor));

        setSaberGlowColor._meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }

    public static void SetNewColor(this SetSaberFakeGlowColor setSaberFakeGlowColor, Color color)
    {
        setSaberFakeGlowColor._parametric3SliceSprite.color = color;
        setSaberFakeGlowColor._parametric3SliceSprite.Refresh();
    }
}