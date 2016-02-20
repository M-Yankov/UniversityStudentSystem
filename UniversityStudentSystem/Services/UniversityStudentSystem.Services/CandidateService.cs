namespace UniversityStudentSystem.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class CandidateService : ICandidateService
    {
        private IRepository<Candidate> candidatesRepository;
        private IRepository<Document> documentsRepository;

        public CandidateService(IRepository<Candidate> candidatesRepo, IRepository<Document> documentsRepo)
        {
            this.candidatesRepository = candidatesRepo;
            this.documentsRepository = documentsRepo;
        }

        public IQueryable<Candidate> GetAll()
        {
            return this.candidatesRepository.All();
        }

        public Document GetDocument(int id)
        {
            return documentsRepository.GetById(id);
        }

        public byte[] GetFileContents(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void Reject(int candidatureId)
        {
            var candidature = this.candidatesRepository.GetById(candidatureId);
            candidature.IsRejected = true;
            this.candidatesRepository.Update(candidature);
            this.candidatesRepository.Save();
        }

        public void Confirm(int candidatureId)
        {
            var candidature = this.candidatesRepository.GetById(candidatureId);
            candidature.IsApproved = true;
            this.candidatesRepository.Update(candidature);
            this.candidatesRepository.Save();
        }
    }
}
