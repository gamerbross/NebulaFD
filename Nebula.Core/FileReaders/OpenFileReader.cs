﻿using Nebula.Core.Data;
using Nebula.Core.Data.PackageReaders;
using Nebula.Core.Memory;
using System.Drawing;
using System.IO.Compression;

namespace Nebula.Core.FileReaders
{
    public class OpenFileReader : IFileReader
    {
        public string Name => "Open File Structure (HTML/XNA)";
        public Dictionary<int, Bitmap> Icons { get { return _icons; } set { _icons = value; } }
        private Dictionary<int, Bitmap> _icons = new Dictionary<int, Bitmap>();

        public string FilePath { get { return _filePath; } set { _filePath = value; } }
        public string _filePath = string.Empty;

        public CCNPackageData Package = new();

        public bool Unpacked;

        public void LoadGame(ByteReader fileReader, string filePath)
        {
            ByteReader? ccnReader = null;
            if (Directory.Exists("Temp"))
                Directory.Delete("Temp", true);
            ZipArchive archive = ZipFile.OpenRead(_filePath = filePath);
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (Directory.GetParent(entry.FullName)?.Name == "resources")
                {
                    if (Path.GetExtension(entry.Name) == ".cc1")
                    {
                        if (File.Exists("openccj.zip"))
                            File.Delete("openccj.zip");
                        if (File.Exists("open.ccj"))
                            File.Delete("open.ccj");
                        entry.ExtractToFile("openccj.zip");
                        ZipArchive ccjarchive = ZipFile.OpenRead("openccj.zip");
                        ccjarchive.Entries.First().ExtractToFile("open.ccj");
                        ccjarchive.Dispose();
                        File.Delete("openccj.zip");
                        ccnReader = new ByteReader(File.ReadAllBytes("open.ccj"));
                        File.Delete("open.ccj");
                    }
                    else if (Path.GetExtension(entry.Name) == ".mp3" ||
                             Path.GetExtension(entry.Name) == ".ogg" ||
                             Path.GetExtension(entry.Name) == ".wav")
                    {
                        // TODO
                    }
                    else if (Path.GetExtension(entry.Name) == ".png")
                    {
                        Directory.CreateDirectory("Temp");
                        entry.ExtractToFile("Temp\\" + (entry.Name[0] == 'M' ? "M" : "I") + int.Parse(Path.GetFileNameWithoutExtension(entry.Name).TrimStart('M')) + ".png");
                    }
                }
            }
            archive.Dispose();

            if (ccnReader != null)
            {
                Package.Read(ccnReader);
                try
                {
                    Directory.Delete("Temp", true);
                }
                catch {}
            }
        }

        public PackageData getPackageData() => Package!;

        public IFileReader Copy()
        {
            CCNFileReader fileReader = new()
            {
                Package = Package,
                Icons = _icons
            };
            return fileReader;
        }
    }
}
