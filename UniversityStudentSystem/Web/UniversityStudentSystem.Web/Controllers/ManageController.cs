namespace UniversityStudentSystem.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models.Enums;
    using Models.Specialties;

    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Services.Contracts;
    using UniversityStudentSystem.Web.HelperProviders;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using UniversityStudentSystem.Web.Models.Candidates;
    using UniversityStudentSystem.Web.Models.Manage;
    using UniversityStudentSystem.Web.Models.Users;

    [Authorize]
    public class ManageController : BaseController
    {
        //// Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;
        private IUserService usersService;
        private ISpecialtiesService specialtiesService;

        public ManageController(IUserService servce, ISpecialtiesService specialties)
        {
            this.usersService = servce;
            this.specialtiesService = specialties;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Index(ManageMessageId? message)
        {
            var userId = User.Identity.GetUserId();

            var model = this.Mapper.Map<UserViewModel>(this.usersService.GetById(userId));
            return this.View(model);
        }

        public ActionResult Edit()
        {
            string userId = this.User.Identity.GetUserId();
            var model = this.Mapper.Map<UserInputModel>(this.usersService.GetById(userId));

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserInputModel model, HttpPostedFileBase file)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string defaultAvatarName = null;

            if (file != null)
            {
                UploadResult result = this.UserManagement.SaveImage(file, this.UserId);
                if (!result.HasSucceed)
                {
                    this.ViewBag.Message = result.Error;
                    return this.View(model);
                }

                defaultAvatarName = result.Path;
            }

            //// TODO: try something:
            User databaseUser = this.usersService.GetById(this.UserId);
            databaseUser.FirstName = model.FirstName;
            databaseUser.LastName = model.LastName;
            databaseUser.Age = model.Age;
            databaseUser.Email = model.Email;
            databaseUser.AboutMe = model.AboutMe;
            databaseUser.SkypeName = model.SkypeName;
            databaseUser.LinkedInProfile = model.LinkedInProfile;
            databaseUser.FacebookAccount = model.FacebookAccount;

            if (defaultAvatarName != null || databaseUser.AvaratUrl != null)
            {
                databaseUser.AvaratUrl = Path.Combine("/Users", this.UserId, defaultAvatarName ?? databaseUser.AvaratUrl);
            }

            this.usersService.Update(databaseUser);

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAvatar()
        {
            this.usersService.ClearAvatar(this.UserId);
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Apply()
        {
            var candidates = this.usersService
                 .GetCandidatures(this.UserId)
                 .OrderByDescending(c => c.DateSent)
                 .To<CandidateViewModel>()
                 .ToList();

            return this.View(candidates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(CandidateInputModel candidature, HttpPostedFileBase file)
        {
            if (file == null || !this.ModelState.IsValid || file.FileName.EndsWith(".exe"))
            {
                return this.RedirectToAction("Apply");
            }

            string docName = $"Apply-for-{ candidature.SpecialtyId }-{ DateTime.Now.ToString("yyyy-MMM-dd") }";
            string path = this.UserManagement.SaveDocument(file, this.UserId, docName);

            this.usersService.MakeApply(this.UserId, candidature.SpecialtyId, path);

            return this.RedirectToAction("Apply");
        }

        [ChildActionOnly]
        public ActionResult ApplyForm()
        {
            if (this.usersService.CanApply(this.UserId))
            {
                CandidateInputModel model = new CandidateInputModel();
                model.Specialties = this.specialtiesService.GetAll().To<SpecialtyViewModel>().ToList();
                return this.PartialView("_ApplyForm", model);
            }

            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await this.UserManager.RemoveLoginAsync(this.User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }

            return this.RedirectToAction("ManageLogins", new { Message = message });
        }

        public ActionResult AddPhoneNumber()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //// Generate the token and send it
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (this.UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };

                await this.UserManager.SmsService.SendAsync(message);
            }

            return this.RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.User.Identity.GetUserId(), true);
            var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await this.UserManager.SetTwoFactorEnabledAsync(this.User.Identity.GetUserId(), false);
            var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction("Index", "Manage");
        }

        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await this.UserManager.GenerateChangePhoneNumberTokenAsync(this.User.Identity.GetUserId(), phoneNumber);
            
            //// Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? this.View("Error") : this.View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //// POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserManager.ChangePhoneNumberAsync(this.User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return this.RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }

            //// If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, "Failed to verify phone");
            return this.View(model);
        }

        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await this.UserManager.SetPhoneNumberAsync(this.User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return this.RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }

            var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            if (user != null)
            {
                await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return this.RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        public ActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = await this.UserManager.ChangePasswordAsync(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }

        public ActionResult SetPassword()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
            }

            //// If we got this far, something failed, redisplay form
            return this.View(model);
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : string.Empty;
            var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
            if (user == null)
            {
                return this.View("Error");
            }

            var userLogins = await this.UserManager.GetLoginsAsync(this.User.Identity.GetUserId());
            var otherLogins = this.AuthenticationManager
                .GetExternalAuthenticationTypes()
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                .ToList();

            this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), this.User.Identity.GetUserId());
        }

        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await this.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, this.User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }

            var result = await this.UserManager.AddLoginAsync(this.User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? this.RedirectToAction("ManageLogins") : this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }

            return false;
        }

        #endregion
    }
}