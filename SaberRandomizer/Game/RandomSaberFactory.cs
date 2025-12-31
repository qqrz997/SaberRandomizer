using System;
using System.Threading;
using System.Threading.Tasks;
using SabersCore.Models;
using SabersCore.Services;

namespace SaberRandomizer.Game;

internal class RandomSaberFactory
{
    private readonly ISaberFileManager saberFileManager;
    private readonly ISaberInstanceFactory saberInstanceFactory;
    private readonly Random random = new();
    
    public RandomSaberFactory(
        ISaberFileManager saberFileManager, 
        ISaberInstanceFactory saberInstanceFactory)
    {
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