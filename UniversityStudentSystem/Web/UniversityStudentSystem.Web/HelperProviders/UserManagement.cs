namespace UniversityStudentSystem.Web.HelperProviders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Common;

    public class UserManagement
    {
        private HttpServerUtilityBase server;

        public UserManagement(HttpServerUtilityBase serverBase)
        {
            this.server = serverBase;
        }

        public void EnsureFolder(string userId)
        {
            string path = this.server.MapPath($"~/Users/{ userId }");
            string uploadsFolder = Path.Combine(path, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
        }

        public string GetCurrentUserDirecotry(string userId)
        {
            return this.server.MapPath($"~/Users/{ userId }");
        }

        public string SaveDocument(HttpPostedFileBase file, string userId, string fileName)
        {
            int indexOfDot = file.FileName.LastIndexOf('.');
            string extenssion = file.FileName.Substring(indexOfDot);

            string userDirectory = this.GetCurrentUserDirecotry(userId);
            string fullFileName = fileName + extenssion;
            string filePath = Path.Combine(userDirectory, "uploads", fullFileName);
            this.EnsureFolder(userId);
            file.SaveAs(filePath);

            return $"~/Users/{ userId }/Uploads/{ fullFileName }";
        }

        public SaveImageResult SaveImage(HttpPostedFileBase file, string userId)
        {
            string[] imageExtensions = WebConstants.AcceptImageProfileTypes
                    .Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

            string fileExtension = file.ContentType.Substring(file.ContentType.LastIndexOf("/") + 1);

            if (!fileExtension.Contains(fileExtension))
            {
                string errorMessage = $"Only images with type .{string.Join(" .", imageExtensions)} are allowed!";
                return new SaveImageResult()
                {
                    Error = errorMessage,
                    HasSucceed = false
                };
            }

            if (file.ContentLength > WebConstants.MaxContentLengthImage)
            {
                string errorMessage = $@"Only images with size less than { WebConstants.MaxContentLengthImage / (1000 * 1000)}MB are allowed!";

                return new SaveImageResult()
                {
                    Error = errorMessage,
                    HasSucceed = false
                };
            }

            string userDirectory = this.GetCurrentUserDirecotry(userId);

            string defaultAvatarName = $"avatar.{ fileExtension }";
            string imagePath = Path.Combine(userDirectory, defaultAvatarName);

            this.EnsureFolder(userId);
            file.SaveAs(imagePath);

            return new SaveImageResult()
            {
                HasSucceed = true,
                Path = defaultAvatarName
            };
        }

        public SaveImageResult SaveSolution(HttpPostedFileBase file, string userId, int courseId)
        {
            string[] archivesExtensions = WebConstants.AcceptArchives
                    .Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

            bool hasCorrectExtenxion = false;
            foreach (var fileExtension in archivesExtensions)
            {
                if (file.FileName.EndsWith(fileExtension))
                {
                    hasCorrectExtenxion = true;
                    break;
                }
            }

            if (!hasCorrectExtenxion)
            {
                string errorMessage =
                    $"Only archives with type .{string.Join(" .", archivesExtensions)} are allowed!";
                return new SaveImageResult()
                {
                    Error = errorMessage,
                    HasSucceed = false
                };
            }

            if (file.ContentLength > WebConstants.MaxContentLengthSolution)
            {
                string errorMessage = $@"Only archives with size less than { WebConstants.MaxContentLengthSolution / (1000 * 1000)}MB are allowed!";

                return new SaveImageResult()
                {
                    Error = errorMessage,
                    HasSucceed = false
                };
            }

            int indexOfDot = file.FileName.IndexOf(".");
            string extension = file.FileName.Substring(indexOfDot + 1);

            string relativePath = $"/Users/{ userId }/Uploads/Solution-for-{ courseId }.{ extension }";
            string fileSystemPath = this.server.MapPath(
                $"~{ relativePath }");

            file.SaveAs(fileSystemPath);

            return new SaveImageResult()
            {
                HasSucceed = true,
                Path = relativePath,
            };
        }
    }
}