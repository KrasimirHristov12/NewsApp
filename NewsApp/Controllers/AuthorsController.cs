using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data.Models;
using NewsApp.Models.Authors;
using NewsApp.Services.Emails;

namespace NewsApp.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly IEmailSenderService emailSender;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthorsController(IEmailSenderService emailSender, UserManager<ApplicationUser> userManager)
        {
            this.emailSender = emailSender;
            this.userManager = userManager;
        }
        public IActionResult Become()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Become(AuthorsInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string subject = "Author candidate";
            string body = @$"<p>First Name: {model.FirstName}</p><br/>
                             <p>Middle Name: {model.MiddleName}</p><br/>
                             <p>Last Name: {model.LastName}</p><br/>
                             <p>Years of experience: {model.YearsOfExperience}</p><br/>
                             <p>Cover letter</p><br/>
                             <p>{model.CoverLetter}</p>
                             <div style=""display: flex; justify-content: space-between;""><a href=""https://localhost:7017/Authors/Approve?des=Yes"">Yes</a><a href=""https://localhost:7017/Authors/Approve?des=No"">No</a></div>";
            await emailSender.SendEmailAsync("theshadow9@mail.bg", "Krasimir", subject, body);
            return Content("Your application is under review! You will be notified once we make a decision.");

        }

        public async Task<IActionResult> Approve(string des)
        {

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            string subject = string.Empty;
            string body = string.Empty;

            if (des == "Yes")
            {
                subject = "Congratulations!";
                body = "Your application was approved! From now on you have author permissions in our website!";

                await userManager.AddToRoleAsync(user, "Author");
            }


            else if (des == "No")
            {
                subject = "Apologies!";
                body = "Thanks for the efforts, however your application was not approved!";
            }
            else
            {
                return BadRequest();
            }
            await emailSender.SendEmailAsync(user.Email, user.Email, subject, body);
            return Content("Operation successful");
        }
    }
}
