namespace UniversityStudentSystem.Services
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Common;
    using UniversityStudentSystem.Services.Contracts;

    public class CertificateService : ICertificateService
    {
        private readonly Point studentNamesPosition = new Point(363, 363);
        private readonly Point specialtyNamePosition = new Point(370, 500);
        private readonly Point awardedOnPositon = new Point(225, 600);
        private readonly Point expiresOnPostion = new Point(700, 600);

        private IRepository<Diploma> diplomsRepository;
        private IRepository<Specialty> specialtiesRepository;
        private IRepository<User, string> usersRepository;

        public CertificateService(
            IRepository<Diploma> diplomsRepo,
            IRepository<Specialty> specialtiesRepo,
            IRepository<User, string> usersRepo)
        {
            this.diplomsRepository = diplomsRepo;
            this.specialtiesRepository = specialtiesRepo;
            this.usersRepository = usersRepo;
        }

        public void GiveToPerson(string userId, int specialtyId, string pathToUserFolder, string pathToCertificate)
        {
            var user = this.usersRepository.GetById(userId);
            var specialty = this.specialtiesRepository.GetById(specialtyId);

            if (user.Diploms.Any(d => d.SpecialtyId == specialtyId))
            {
                return;
            }

            var image = this.MakeCeritficate(
                pathToCertificate,
                user.FirstName + " " + user.LastName,
                specialty.Name,
                DateTime.Now,
                DateTime.Now.AddYears(1));

            string certificatePath = System.IO.Path.Combine(pathToUserFolder, "Certificate-" + specialty.Name + ".jpg");
            certificatePath = certificatePath.Replace(" ", "-");
            image.Save(certificatePath, ImageFormat.Jpeg);
            string databasePath = this.GetUserFolderPath(certificatePath);

            this.diplomsRepository.Add(new Diploma()
            {
                CreatedOn = DateTime.Now,
                Name = user.FirstName + " " + user.LastName + " - " + specialty.Name,
                UserId = user.Id,
                SpecialtyId = specialty.Id,
                PathToImage = databasePath
            });

            this.diplomsRepository.Save();
        }

        public byte[] MakeCertificate(
            string pathToImage,
            string studentName,
            string specialtyName,
            DateTime awardedOn,
            DateTime expiresOn)
        {
            Bitmap certificate = this.MakeCeritficate(
                 pathToImage,
                 studentName,
                 specialtyName,
                 awardedOn,
                 expiresOn);

            return (byte[])new ImageConverter().ConvertTo(certificate, typeof(byte[]));
        }

        private Bitmap MakeCeritficate(
           string pathToImage,
           string studentName,
           string specialtyName,
           DateTime awardedOn,
           DateTime expiresOn)
        {
            Bitmap image = (Bitmap)Image.FromFile(pathToImage);
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawString(
                    studentName,
                    new Font(
                        CertificateConstants.DefaultFontFamily,
                        CertificateConstants.FontSizeRegular,
                        FontStyle.Regular),
                    Brushes.Black,
                    this.studentNamesPosition);

                graphics.DrawString(
                    specialtyName,
                    new Font(
                        CertificateConstants.DefaultFontFamily,
                        CertificateConstants.FontSizeBigger,
                        FontStyle.Regular),
                    Brushes.Black,
                    this.specialtyNamePosition);

                graphics.DrawString(
                    awardedOn.ToString(CertificateConstants.DateFormat),
                    new Font(
                        CertificateConstants.DefaultFontFamily,
                        CertificateConstants.FontSizeSmaller,
                        FontStyle.Regular),
                    Brushes.Black,
                    this.awardedOnPositon);

                graphics.DrawString(
                    expiresOn.ToString(CertificateConstants.DateFormat),
                    new Font(
                        CertificateConstants.DefaultFontFamily,
                        CertificateConstants.FontSizeSmaller,
                        FontStyle.Regular),
                        Brushes.Black,
                        this.expiresOnPostion);
            }

            return image;
        }

        private string GetUserFolderPath(string path)
        {
            int index = path.LastIndexOf("\\Users");
            return path.Substring(index);
        }
    }
}
