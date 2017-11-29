using System;
using System.IO;

namespace ProNet
{
    public class FileService : IFileService
    {
        public Stream GetContents(string filePath)
        {
            try
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (FileNotFoundException e)
            {
                throw new ArgumentException("File not found");
            }
        }
    }
}