using System.Net;
using System.Net.Http.Headers;
using EM.DataRepository.Clients;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EM.Web.Areas.Client.Pages
{
    public class IndexModel : PageModel
    {
        public IList<ListClients>? Clients { get; set; } = new List<ListClients>();

        [BindProperty] public CreateClient CreateClient { get; set; } = new CreateClient();
        [BindProperty] public CreateClient EditClient { get; set; } = new CreateClient();

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await Api.GetApi("Client");

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    Clients = JsonConvert.DeserializeObject<IList<ListClients>>(result);
                    return Page();
                case HttpStatusCode.NoContent:
                    return Page();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnGetClientInfoAsync(string nif)
        {
            var response = await Api.GetApi("Client/" + nif);

            var result = await response.Content.ReadAsStringAsync();

            var clientDetails = JsonConvert.DeserializeObject<InfoClient>(result);

            var resultJson = new
            {
                data1 = clientDetails?.Nif,
                data2 = clientDetails?.Name,
                data3 = clientDetails?.Address1,
                data4 = clientDetails?.Address2,
                data5 = clientDetails?.PostalCode,
                data6 = clientDetails?.City,
                data7 = clientDetails?.NameResponsible,
                data8 = clientDetails?.ContactResponsible,
                data9 = clientDetails?.EmailResponsible
            };

            return new JsonResult(resultJson);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateClient, "Client");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var response = await Api.PutApi(EditClient, "Client");

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }

        public async Task OnGetDeleteAsync(string nif)
        {
            await Api.DeleteApi("Client/" + nif);
        }
    }
}