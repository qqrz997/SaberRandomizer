using System.Threading;
using System.Threading.Tasks;
using SabersCore.Models;

namespace SaberRandomizer.Game;

internal class GameplaySaberProvider
{
    private readonly RandomSaberFactory saberFactory;
    
    public GameplaySaberProvider(RandomSaberFactory saberFactory)
    {
        this.saberFactory = saberFactory;
    }

    private Task<SaberInstanceSet>? saberInstance;

    public async Task<SaberInstanceSet> GetSabers()
    {
        saberInstance ??= saberFactory.CreateRandomSaber();
        return await saberInstance;
    }
}