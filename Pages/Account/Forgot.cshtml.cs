using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRazor.Helper;
using WebRazor.Models;

namespace WebRazor.Pages.Account
{
    public class ForgotModel : PageModel
    {
        private readonly PRN221_DBContext dbContext;
        private const string SECRET_KEY = "HL1405";

        public ForgotModel(PRN221_DBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [BindProperty]
        public string email { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (String.IsNullOrEmpty(email))
            {
                ViewData["msg"] = "Please enter your email to get password!";
                return Page();
            }

            var account = await dbContext.Accounts.FirstOrDefaultAsync(a => a.Email.Equals(email));
            if (account == null)
            {
                ViewData["msg"] = "Not found email in for system, please check again!";
                return Page();
            }

            string passwordGenerate = AccountHelper.GeneratePassword(8);
            account.Password = AccountHelper.HashPassWord(SECRET_KEY + passwordGenerate);

            dbContext.Update(account);
            if (await dbContext.SaveChangesAsync() <= 0)
            {
                ViewData["msg"] = "System error, please try again!";
                return Page();
            }

            string bodyMail = "Your new password is: " + passwordGenerate;
            MailHelper.SendMail(email, bodyMail, null);
            ViewData["msg"] = "Please check you email!";
            return Page();
        }
    }
}
