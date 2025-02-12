using SaberRandomizer.Menu;
using Zenject;

namespace SaberRandomizer.Installers;

internal class MenuInstaller : Installer
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<TabManager>().AsSingle();
        Container.Bind<GameplaySetupTab>().AsSingle();
    }
}