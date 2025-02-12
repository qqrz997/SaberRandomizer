using System.IO;
using System.Threading.Tasks;
using AssetBundleLoadingTools.Utilities;
using UnityEngine;

namespace SaberRandomizer.App;

internal class CustomSaberLoader
{
    private readonly SaberPrefabCache saberPrefabCache;

    public CustomSaberLoader(SaberPrefabCache saberPrefabCache)
    {
        this.saberPrefabCache = saberPrefabCache;
    }
    
    public async Task<GameObject?> LoadSaber(FileInfo saberFile)
    {
        if (saberPrefabCache.TryGetPrefab(saberFile, out var cached)) return cached;
        
        if (!saberFile.Exists) return null;
        
        await using var fileStream = saberFile.OpenRead();

        AssetBundle? assetBundle;
        
        if (!fileStream.CanRead || !fileStream.CanSeek)
        {
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            assetBundle = await AssetBundleExtensions.LoadFromStreamAsync(memoryStream);
        }
        else
        {
            assetBundle = await AssetBundleExtensions.LoadFromStreamAsync(fileStream);
        }
        
        if (assetBundle == null) return null;
        
        var saberPrefab = await AssetBundleExtensions.LoadAssetAsync<GameObject>(assetBundle, "_CustomSaber");
        
        if (saberPrefab == null) return null;

        saberPrefabCache.TryAddPrefab(saberFile, saberPrefab);
        
        assetBundle.Unload(false);
        
        return saberPrefab;
    }
}