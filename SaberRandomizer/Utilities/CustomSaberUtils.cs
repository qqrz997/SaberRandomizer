using System.Collections.Generic;
using System.Linq;
using CustomSaber;
using SaberRandomizer.Models;
using UnityEngine;
using static SaberRandomizer.Utilities.TrailUtils;

namespace SaberRandomizer.Utilities;

internal static class CustomSaberUtils
{
    /// <summary>
    /// Searches a CustomSaber's GameObject for any custom trails.
    /// </summary>
    /// <param name="saberObject">The GameObject of the custom saber</param>
    public static ITrailData[] GetTrailsFromCustomSaber(GameObject saberObject) => saberObject
        .GetComponentsInChildren<CustomTrail>()
        .Where(IsCustomTrailValid)
        .Select(trail => trail.ToTrailData(saberObject))
        .ToArray();
    
    private static bool IsCustomTrailValid(CustomTrail ct) => ct.PointEnd != null && ct.PointStart != null;
    
    private static ITrailData ToTrailData(this CustomTrail ct, GameObject saberObject) => new CustomTrailData(
        material: ct.TrailMaterial,
        lengthSeconds: ConvertLegacyLength(ct.Length),
        colorType: ct.colorType,
        customColor: ct.TrailColor,
        colorMultiplier: ct.MultiplierColor,
        trailTopOffset: ct.PointEnd.position - saberObject.transform.position,
        trailBottomOffset: ct.PointStart.position - saberObject.transform.position);
    
    /// <summary>
    /// Searches a GameObject's renderers for any materials which can be recolored.
    /// </summary>
    /// <param name="saberObject">The GameObject of the custom saber</param>
    /// <returns>An array of found <see cref="Material"/>s.</returns>
    public static Material[] GetColorableSaberMaterials(GameObject saberObject)
    {
        var colorableMaterials = new List<Material>();
        foreach (var renderer in saberObject.GetComponentsInChildren<Renderer>(true))
        {
            if (renderer == null) continue;

            var materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].IsColorable())
                {
                    materials[i] = new(materials[i]);
                    renderer.sharedMaterials = materials;
                    colorableMaterials.Add(materials[i]);
                }
            }
        }
        return colorableMaterials.ToArray();
    }
    
    private static bool IsColorable(this Material material) =>
        material != null && material.HasProperty(MaterialProperties.Color) && material.HasColorableProperty();

    private static bool HasColorableProperty(this Material material) =>
        material.HasProperty(MaterialProperties.CustomColors) ? material.GetFloat(MaterialProperties.CustomColors) > 0
            : material.HasGlowOrBloom();

    private static bool HasGlowOrBloom(this Material material) =>
        material.HasProperty(MaterialProperties.Glow) && material.GetFloat(MaterialProperties.Glow) > 0
        || material.HasProperty(MaterialProperties.Bloom) && material.GetFloat(MaterialProperties.Bloom) > 0;
}