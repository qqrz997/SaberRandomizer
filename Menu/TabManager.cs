using System;
using BeatSaberMarkupLanguage.GameplaySetup;
using Zenject;

namespace SaberRandomizer.Menu;

internal class TabManager : IInitializable, IDisposable
{
    private readonly GameplaySetup gameplaySetup;
    private readonly GameplaySetupTab gameplaySetupTab;

    public TabManager(
        GameplaySetup gameplaySetup,
        GameplaySetupTab gameplaySetupTab)
    {
        this.gameplaySetup = gameplaySetup;
        this.gameplaySetupTab = gameplaySetupTab;
    }
    
    public void Initialize()
    {
        gameplaySetup.AddTab("Saber Randomizer", "SaberRandomizer.Menu.gameplaySetup.bsml", gameplaySetupTab);
    }

    public void Dispose()
    {
        gameplaySetup.RemoveTab("Saber Randomizer");
    }
}