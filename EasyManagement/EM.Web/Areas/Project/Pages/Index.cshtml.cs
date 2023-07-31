using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.DataRepository.Products;
using EM.DataRepository.Projects;
using EM.DataRepository.Suppliers;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace EM.Web.Areas.Project.Pages
{
    public class IndexModel : PageModel
    {
        public List<SelectListItem> IdClients { get; set; }
        public IList<ListProjects>? Projects { get; set; } = new List<ListProjects>();
        [BindProperty] public CreateProject CreateProject { get; set; } = new CreateProject();

        public async Task<IActionResult> OnGet()
        {
            var response = await Api.GetApi("Client");
            var result = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<List<ListClients>>(result);

            IdClients = clients.Select(a =>
            new SelectListItem
            {
                Value = a.Nif.ToString(),
                Text = a.Nif.ToString() + " - " + a.Name
            }).ToList();

            response = await Api.GetApi("Project");
            result = await response.Content.ReadAsStringAsync();
            Projects = JsonConvert.DeserializeObject<IList<ListProjects>>(result);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Page();
                case HttpStatusCode.NoContent:
                    return Page();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateProject, "Project");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnGetRecuseAsync(string id)
        {
            var response = await Api.GetApi("Project/Recuse/" + id);
            
            return new JsonResult(null);
        }

        public async Task<IActionResult> OnGetApproveAsync(string id)
        {
            var response = await Api.GetApi("Project/Approve/" + id);

            return new JsonResult(null);
        }
    }
}
