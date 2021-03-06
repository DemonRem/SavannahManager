﻿using System.IO;
using System.IO.Compression;

namespace Archive
{
    public static class Zip
    {
        public static void Extract(string zipPath, string extractDirPath)
        {
            var directoryInfo = Directory.CreateDirectory(extractDirPath);

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    var outPath = entry.FullName;
                    if (outPath.EndsWith("/"))
                    {
                        var di = new DirectoryInfo(extractDirPath + @"\" + outPath);
                        if (!di.Exists)
                            di.Create();
                    }
                    else
                    {
                        entry.ExtractToFile(Path.Combine(extractDirPath, entry.FullName), true);
                    }
                }
            }
        }
    }
}
