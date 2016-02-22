namespace UniversityStudentSystem.Services.Contracts
{
    public interface IFileService
    {
        byte[] GetFileContents(string path);
    }
}
