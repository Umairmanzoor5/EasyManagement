using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EM.DataRepository.Accounts;
using EM.Helper;
using System.Net;
using EM.DataRepository.Emails;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace EM.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {        
        [BindProperty] public LoginAccount Login { get; set; } = new LoginAccount();
        [BindProperty] public SendEmail SendEmail { get; set; } = new SendEmail();
        //[BindProperty] public string Token { get; set; }

       
        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(Login, "Account/Login");

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:                    
                    return RedirectToPage("/Index");
                default:
                    return Page();
            }            
        }

        public async Task<IActionResult> OnPostRecoverPasswordAsync()
        {
            var response = await Api.PostApi(SendEmail, "Email");

            return response.StatusCode switch
            {
                HttpStatusCode.Created => RedirectToPage(),
                _ => Page(),
            };
        }

       /* public async Task<IActionResult> OnPostConfirmTokenAsync()
        {
            var response = await Api.PostApi(Token, "Account/RecoverPasswordConfirmToken");

            return response.StatusCode switch
            {
                HttpStatusCode.Accepted => RedirectToPage(), // Redirect to recover password page after token is confirmed where it's possible to edit the password
                _ => Page(),
            };
        }*/

        /*public void OnPut()
        {

        }*/
        /*public void OnDelete()
        {

        }*/
    }
}
