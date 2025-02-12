using SaberRandomizer.App;
using Zenject;

namespace SaberRandomizer.Installers;

internal class AppInstaller : Installer
{
    private readonly PluginConfig pluginConfig;

    public AppInstaller(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameResourcesProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<SaberPrefabCache>().AsSingle();
        Container.BindInterfacesAndSelfTo<FileManager>().AsSingle();
        Container.BindInstance(pluginConfig).AsSingle();
        Container.Bind<CustomSaberLoader>().AsSingle();
        Container.Bind<TrailFactory>().AsSingle();
        Container.Bind<SaberFactory>().AsSingle();
    }
}

