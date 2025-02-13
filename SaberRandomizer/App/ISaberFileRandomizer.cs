using System.IO;

namespace SaberRandomizer.App;

public interface ISaberFileRandomizer
{
    public FileInfo? GetRandomSaberFile();
}