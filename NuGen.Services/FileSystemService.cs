using System.IO;
using NuGen.Services.Interfaces;

namespace NuGen.Services
{
    public class FileSystemService : IFileSystemService
    {
        public void CreateDirIfNotExists(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (dir != null && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public StreamWriter CreateStreamWriter(string path)
        {
            if (!File.Exists(path))
            {
                return File.CreateText(path);
            }

            return new StreamWriter(path);
        }

        public string AddPrefixToFile(string path, string prefix)
        {
            return Path.Combine(Path.GetDirectoryName(path) ?? ".", prefix + Path.GetFileName(path));
        }
    }
}