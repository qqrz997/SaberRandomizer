using SaberRandomizer.Game;
using SiraUtil.Sabers;
using Zenject;

namespace SaberRandomizer.Installers;

internal class PlayerInstaller : Installer
{
    private readonly PluginConfig pluginConfig;
    
    public PlayerInstaller(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }
    
    public override void InstallBindings()
    {
        if (!pluginConfig.ModifierEnabled) return;

        Container.Bind<GameplaySaberProvider>().AsSingle();
        Container.Bind<RandomSaberFactory>().AsSingle();
        
        // This replaces the default sabers
        var saberModelRegistration = SaberModelRegistration.Create<RandoSaberModelController>(int.MaxValue);
        Container.BindInstance(saberModelRegistration);
    }
}