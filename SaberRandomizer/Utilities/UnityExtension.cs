using UnityEngine;

namespace SaberRandomizer.Utilities;

internal static class UnityExtension
{
    public static T GetComponentOrAdd<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        if (component == null) component = gameObject.AddComponent<T>();
        return component;
    }
    
    public static void SetLayerRecursively(this GameObject obj, int layer) => SetLayer(obj, layer);
    private static void SetLayer(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            SetLayer(gameObject.transform.GetChild(i).gameObject, layer);
        }
    }
}