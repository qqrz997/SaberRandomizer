using System;
using System.Reflection;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SaberRandomizer.Installers;
using SaberRandomizer.Utilities;
using SiraUtil.Zenject;
using IpaLogger = IPA.Logging.Logger;

namespace SaberRandomizer;

[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
internal class Plugin
{
    private readonly IpaLogger logger;
    
    [Init]
    public Plugin(IpaLogger ipaLogger, Zenjector zenjector, Config config)
    {
        logger = ipaLogger;
        
        if (!TryLoadCustomSaberAssembly()) return;
        
        zenjector.UseLogger(logger);
        zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>());
        zenjector.Install<MenuInstaller>(Location.Menu);
        zenjector.Install<PlayerInstaller>(Location.Player);
    }

    private bool TryLoadCustomSaberAssembly()
    {
        try
        {
            Assembly.Load(ResourceLoading.GetResource("SaberRandomizer.Resources.CustomSaber.dll"));
            return true;
        }
        catch (Exception e)
        {
            logger.Critical($"Couldn't initialize Saber Randomizer. Failed to load CustomSaber.dll\n{e}");
            return false;
        }
    }
}