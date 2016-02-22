namespace UniversityStudentSystem.Services.Contracts
{
    using System;

    public interface ICertificateService
    {
        void GiveToPerson(string userId, int specialtyId, string pathToUserFolder, string pathToCertificate);

        void GiveToAllFromSpecialty(int specialtyId, string pathToUsersFolder, string pathToCertificate);

        byte[] MakeCertificate(
            string pathToImage,
            string studentName, 
            string specialtyName, 
            DateTime awardedOn,
            DateTime expiresOn);
    }
}
