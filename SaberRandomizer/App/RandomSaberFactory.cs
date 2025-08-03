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
    private readonly ISaberInstanceFactory saberInstanceFactory;
    
    public RandomSaberFactory(
        Random random,
        ISaberFileManager saberFileManager, 
        ISaberInstanceFactory saberInstanceFactory)
    {
        this.random = random;
        this.saberFileManager = saberFileManager;
        this.saberInstanceFactory = saberInstanceFactory;
    }
    
    public async Task<SaberInstanceSet> CreateRandomSaber()
    {
        var files = saberFileManager.GetLoadedSaberFiles();
        if (files is [])
        {
            return await saberInstanceFactory.CreateSaberSet(null, CancellationToken.None);
        }
        
        var randomIndex = random.Next(files.Length);
        var saberFile = files[randomIndex];
        return await saberInstanceFactory.CreateSaberSet(saberFile.Hash, CancellationToken.None);
    }
}