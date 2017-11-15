using System.IO;

namespace ProNet
{
    public interface IFileService
    {
        Stream GetContents(string filePath);
    }
}