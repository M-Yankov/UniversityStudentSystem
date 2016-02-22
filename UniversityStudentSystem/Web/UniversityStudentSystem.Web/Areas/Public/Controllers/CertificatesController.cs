namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using UniversityStudentSystem.Common;
    using Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    public class CertificatesController : BaseController
    {
        private IFileService fileService;

        public CertificatesController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public ActionResult Index()
        {
            Bitmap image1 = (Bitmap)Image.FromFile(this.Server.MapPath(WebConstants.PathToCertificate));
            using (Graphics g = Graphics.FromImage(image1))
            {
                g.DrawString("ExampleName", new Font("Arial", 15f, FontStyle.Regular), Brushes.Black, new Point(200, 200));
                //g.DrawLine(new Pen(Color.Black, 30f), new Point(200, 200), new Point(500, 500));
            }
            image1.Save(this.Server.MapPath(WebConstants.PathToCertificate) + "Example.jpg", ImageFormat.Jpeg);
            var obj = (byte[])new ImageConverter().ConvertTo(image1, typeof(byte[]));
            //var image = new ImageCertificate()
            //{
            //    Data = this.fileService
            //        .GetFileContents()
            //};

            return View(new ImageCertificate() { Data = obj });
        }
    }
}