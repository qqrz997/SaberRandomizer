using SaberRandomizer.App;
using SaberRandomizer.Game;
using SiraUtil.Sabers;
using Zenject;

namespace SaberRandomizer.Installers;

internal class PlayerInstaller : Installer
{
    private readonly SaberFactory saberFactory;
    private readonly PluginConfig pluginConfig;
    
    public PlayerInstaller(
        SaberFactory saberFactory,
        PluginConfig pluginConfig)
    {
        this.saberFactory = saberFactory;
        this.pluginConfig = pluginConfig;
    }
    
    public override void InstallBindings()
    {
        if (!pluginConfig.ModifierEnabled) return;
        
        Container.BindInterfacesAndSelfTo<SaberEventService>().AsTransient();
        Container.BindInstance(saberFactory.CreateCurrentSabers()).AsSingle();
        
        // This replaces the default sabers
        Container.BindInstance(SaberModelRegistration.Create<RandoSaberModelController>(int.MaxValue));
    }
}