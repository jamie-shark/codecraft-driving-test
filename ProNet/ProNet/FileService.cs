using System.IO;

namespace ProNet
{
    public class FileService : IFileService
    {
        private readonly string _file;

        public FileService(string file)
        {
            _file = file;
        }

        public Stream GetContents()
        {
            return new FileStream(_file, FileMode.Open, FileAccess.Read);
        }
    }
}