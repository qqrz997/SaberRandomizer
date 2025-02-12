using System;
using System.IO;
using System.Linq;
using IPA.Utilities;
using SiraUtil.Logging;
using Zenject;

namespace SaberRandomizer.App;

internal class FileManager : IInitializable, ISaberFileRandomizer
{
    private readonly Random random;
    private readonly DirectoryInfo customSabersDirectory = new(Path.Combine(UnityGame.InstallPath, "CustomSabers"));
    private readonly string[] supportedFileTypes = ["saber"];

    public FileManager(SiraLog logger, Random random)
    {
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
        var saberFile = saberFiles.Any() ? saberFiles[random.Next(saberFiles.Length - 1)] : null;
        saberFile?.Refresh();

        return saberFile;
    }
}