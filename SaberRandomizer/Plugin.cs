using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SaberRandomizer.Installers;
using SiraUtil.Zenject;

namespace SaberRandomizer;

[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
internal class Plugin
{
    [Init]
    public Plugin(Logger logger, Zenjector zenjector, Config config)
    {
        zenjector.UseLogger(logger);
        zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>());
        zenjector.Install<MenuInstaller>(Location.Menu);
        zenjector.Install<PlayerInstaller>(Location.Player);
    }
}