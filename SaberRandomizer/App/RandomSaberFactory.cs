using System;
using System.Threading;
using System.Threading.Tasks;
using SabersCore.Models;
using SabersCore.Services;

namespace SaberRandomizer.App;

internal class RandomSaberFactory
{
    private readonly Random random;
    private readonly ISaberFileManager saberFileManager;
    private readonly ISaberMetadataCache saberMetadataCache;
    private readonly ISabersLoader sabersLoader;
    private readonly ITrailFactory trailFactory;

    public RandomSaberFactory(
        Random random,
        ISaberFileManager saberFileManager,
        ISaberMetadataCache saberMetadataCache, 
        ISabersLoader sabersLoader, 
        ITrailFactory trailFactory)
    {
        this.random = random;
        this.saberFileManager = saberFileManager;
        this.saberMetadataCache = saberMetadataCache;
        this.sabersLoader = sabersLoader;
        this.trailFactory = trailFactory;
    }
    
    public async Task<SaberInstanceSet> CreateRandomSaber()
    {
        var files = saberFileManager.GetLoadedSaberFiles();
        if (files is [])
        {
            return CreateDefaultSabers();
        }
        
        var randomIndex = random.Next(files.Length);
        var saberFile = files[randomIndex];
        var saberPrefab = !saberMetadataCache.TryGetMetadata(saberFile.Hash, out var meta) ? null
            : await sabersLoader.GetSaberData(meta.SaberFile, true, CancellationToken.None);


        return saberPrefab?.Prefab?.Instantiate() ?? CreateDefaultSabers();
    }

    private SaberInstanceSet CreateDefaultSabers() =>
        new(gameResourcesProvider.CreateNewDefaultSaber(),
            gameResourcesProvider.CreateNewDefaultSaber(),
            [new DefaultTrailData(gameResourcesProvider.DefaultTrailMaterial)],
            [new DefaultTrailData(gameResourcesProvider.DefaultTrailMaterial)]);
}