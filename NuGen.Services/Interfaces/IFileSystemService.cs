using System.IO;

namespace NuGen.Services.Interfaces
{
    public interface IFileSystemService
    {
        void CreateDirIfNotExists(string path);
        StreamWriter CreateStreamWriter(string path);
        string AddPrefixToFile(string path, string prefix);
    }
}