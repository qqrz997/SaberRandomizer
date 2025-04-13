using System;
using System.IO;
using System.Linq;
using IPA.Utilities;
using SiraUtil.Logging;
using Zenject;

namespace SaberRandomizer.App;

internal class FileManager : IInitializable, ISaberFileRandomizer
{
    private readonly SiraLog logger;
    private readonly Random random;
    
    private readonly DirectoryInfo customSabersDirectory = new(Path.Combine(UnityGame.InstallPath, "CustomSabers"));
    private readonly string[] supportedFileTypes = ["saber"];

    public FileManager(SiraLog logger, Random random)
    {
        this.logger = logger;
        this.random = random;
    }
    
    private FileInfo[] saberFiles = [];

    public void Initialize()
    {
        saberFiles = supportedFileTypes
            .SelectMany(fExt => customSabersDirectory.EnumerateFiles($"*.{fExt}", SearchOption.AllDirectories))
            .ToArray();
    }
    
    public FileInfo? GetRandomSaberFile()
    {
        if (saberFiles is [])
        {
            logger.Debug("Couldn't get random saber; there are no saber files.");
            return null;
        }
        
        var saberIndex = random.Next(saberFiles.Length);
        var saberFile = saberFiles[saberIndex];
        
        saberFile.Refresh();
        logger.Debug($"Got saber file at index {saberIndex}: {saberFile.Name}");

        return saberFile;
    }
}