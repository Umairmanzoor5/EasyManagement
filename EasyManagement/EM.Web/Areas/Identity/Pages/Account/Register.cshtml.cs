using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using EM.DataRepository.Accounts;
using EM.Helper;
using System.Net;

namespace EM.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty] public RegisterAccount Register { get; set; } = new RegisterAccount();
        
        /*public void OnGet()
        {

        }*/
        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(Register, "Account");

            return response.StatusCode switch
            {
                HttpStatusCode.Created => RedirectToPage(),
                _ => Page(),
            };
        }
        /*public void OnPut()
        {

        }*/
        /*public void OnDelete()
        {

        }*/
    }
}
