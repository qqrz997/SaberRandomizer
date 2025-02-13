using System.Threading.Tasks;
using SaberRandomizer.Models;
using static SaberRandomizer.Utilities.CustomSaberUtils;
using static UnityEngine.Object;

namespace SaberRandomizer.App;

internal class SaberFactory
{
    private readonly ISaberFileRandomizer saberFileRandomizer;
    private readonly CustomSaberLoader customSaberLoader;
    private readonly GameResourcesProvider gameResourcesProvider;

    public SaberFactory(
        ISaberFileRandomizer saberFileRandomizer,
        CustomSaberLoader customSaberLoader, 
        GameResourcesProvider gameResourcesProvider)
    {
        this.saberFileRandomizer = saberFileRandomizer;
        this.customSaberLoader = customSaberLoader;
        this.gameResourcesProvider = gameResourcesProvider;
    }
    
    public async Task<SaberInstanceSet> CreateCurrentSabers()
    {
        var randomSaber = saberFileRandomizer.GetRandomSaberFile();
        if (randomSaber is null) return CreateDefaultSabers();

        var saberPrefab = await customSaberLoader.LoadSaber(randomSaber);
        if (saberPrefab == null) return CreateDefaultSabers();

        var saber = Instantiate(saberPrefab);
        var leftSaber = saber.transform.Find("LeftSaber");
        var rightSaber = saber.transform.Find("RightSaber");

        return leftSaber == null || rightSaber == null
            ? CreateDefaultSabers()
            : new(
                leftSaber.gameObject,
                rightSaber.gameObject,
                GetTrailsFromCustomSaber(leftSaber.gameObject),
                GetTrailsFromCustomSaber(rightSaber.gameObject));
    }

    private SaberInstanceSet CreateDefaultSabers() =>
        new(gameResourcesProvider.CreateNewDefaultSaber(),
            gameResourcesProvider.CreateNewDefaultSaber(),
            [new DefaultTrailData(gameResourcesProvider.DefaultTrailMaterial)],
            [new DefaultTrailData(gameResourcesProvider.DefaultTrailMaterial)]);
}