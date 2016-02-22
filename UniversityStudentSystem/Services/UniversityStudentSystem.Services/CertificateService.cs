namespace UniversityStudentSystem.Services
{
    using System;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;
    using UniversityStudentSystem.Common;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    public class CertificateService : ICertificateService
    {
        private IRepository<Diploma> diplomsRepository;
        private IRepository<Specialty> specialtiesRepository;
        private IRepository<User, string> usersRepository;

        private readonly Point studentNamesPosition = new Point(363, 363);
        private readonly Point specialtyNamePosition = new Point(370, 500);
        private readonly Point awardedOnPositon = new Point(225, 600);
        private readonly Point expiresOnPostion = new Point(700, 600);

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
                specialty.Name , 
                DateTime.Now, 
                DateTime.Now.AddYears(1));

            string certificatePath = System.IO.Path.Combine(pathToUserFolder, "Certificate-" + specialty.Name + ".jpg");
            certificatePath  = certificatePath.Replace(" ", "-");
            image.Save(certificatePath, ImageFormat.Jpeg);
            string dbPath = this.GetUserFolderPath(certificatePath);

            this.diplomsRepository.Add(new Diploma()
            {
                CreatedOn = DateTime.Now,
                Name = user.FirstName + " " + user.LastName + " - " + specialty.Name,
                UserId = user.Id,
                SpecialtyId = specialty.Id,
                PathToImage = dbPath
            });

            this.diplomsRepository.Save();
        }

        private string GetUserFolderPath(string path)
        {
            int index = path.LastIndexOf("\\Users");
            return path.Substring(index);
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

            //image1.Save(this.Server.MapPath(WebConstants.PathToCertificate) + "Example.jpg", ImageFormat.Jpeg);
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
                    new Font(CertificateConstants.DefaultFontFamily,
                    CertificateConstants.FontSizeSmaller,
                    FontStyle.Regular),
                    Brushes.Black,
                    this.expiresOnPostion);
            }

            return image;
        }
    }
}
