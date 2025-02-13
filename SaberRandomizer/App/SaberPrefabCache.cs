using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using UnityEngine;

namespace SaberRandomizer.App;

internal class SaberPrefabCache
{
    // key is file path
    private readonly Dictionary<string, GameObject> cache = [];

    public bool TryGetPrefab(FileInfo saberFile, [NotNullWhen(true)] out GameObject? prefab) => 
        cache.TryGetValue(saberFile.FullName, out prefab);
    
    public bool TryAddPrefab(FileInfo saberFile, GameObject prefab) => 
        cache.TryAdd(saberFile.FullName, prefab);
}