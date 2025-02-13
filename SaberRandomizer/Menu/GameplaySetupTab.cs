namespace SaberRandomizer.Menu;

internal class GameplaySetupTab
{
    private readonly PluginConfig pluginConfig;

    public GameplaySetupTab(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public bool ModifierEnabled
    {
        get => pluginConfig.ModifierEnabled;
        set => pluginConfig.ModifierEnabled = value;
    }
}