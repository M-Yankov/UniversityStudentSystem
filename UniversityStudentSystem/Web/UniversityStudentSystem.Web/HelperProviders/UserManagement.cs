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

            // TODO: Ensure other paths like: Users/UserId/Uploads/  
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GetCurrentUserDirecotry(string userId)
        {
            return this.server.MapPath($"~/Users/{ userId }");
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
            file.SaveAs(imagePath);

            return new SaveImageResult()
            {
                HasSucceed = true,
                Path = defaultAvatarName
            };
        }
    }
}