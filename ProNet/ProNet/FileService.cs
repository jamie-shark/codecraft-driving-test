using System.IO;

namespace ProNet
{
    public class FileService : IFileService
    {
        public Stream GetContents(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }
    }
}