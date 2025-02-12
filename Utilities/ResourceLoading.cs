using System;
using System.IO;
using System.Reflection;

namespace SaberRandomizer.Utilities;

internal class ResourceLoading
{
    private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

    public static byte[] GetResource(string resourcePath)
    {
        using var stream = Assembly.GetManifestResourceStream(resourcePath);
        if (stream is null) throw new NullReferenceException("Resource not found");

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}