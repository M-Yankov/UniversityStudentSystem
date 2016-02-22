namespace UniversityStudentSystem.Services
{
    using System.IO;
    using Contracts;

    public class FileService : IFileService
    {
        public byte[] GetFileContents(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}
