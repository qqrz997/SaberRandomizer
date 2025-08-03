using SaberRandomizer.App;
using SaberRandomizer.Game;
using SiraUtil.Sabers;
using Zenject;

namespace SaberRandomizer.Installers;

internal class PlayerInstaller : Installer
{
    private readonly RandomSaberFactory randomSaberFactory;
    private readonly PluginConfig pluginConfig;
    
    public PlayerInstaller(
        RandomSaberFactory randomSaberFactory,
        PluginConfig pluginConfig)
    {
        this.randomSaberFactory = randomSaberFactory;
        this.pluginConfig = pluginConfig;
    }
    
    public override void InstallBindings()
    {
        if (!pluginConfig.ModifierEnabled) return;
        
        Container.BindInstance(randomSaberFactory.CreateRandomSaber()).AsSingle();
        
        // This replaces the default sabers
        Container.BindInstance(SaberModelRegistration.Create<RandoSaberModelController>(int.MaxValue));
    }
}