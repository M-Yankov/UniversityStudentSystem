namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface ICandidateService
    {
        IQueryable<Candidate> GetAll();

        byte[] GetFileContents(string path);

        Document GetDocument(int id);

        void Confirm(int candidatureId);

        void Reject(int candidatureId);
    }
}
