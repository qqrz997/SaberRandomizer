using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SaberRandomizer;

internal class PluginConfig
{
    public virtual bool ModifierEnabled { get; set; } = false;
}