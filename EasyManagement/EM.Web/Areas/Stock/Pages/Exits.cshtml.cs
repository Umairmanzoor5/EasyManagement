using System.Net;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.DataRepository.Clients;
using EM.DataRepository.Projects;
using EM.DataRepository.Warehouses;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EM.Web.Areas.Stock.Pages
{
    public class ExitsModel : PageModel
    {
        [BindProperty] public Exit ExitProducts { get; set; }


        public List<SelectListItem> Clients { get; set; }
        public List<SelectListItem> IdsProducts { get; set; }
        public List<SelectListItem> Products { get; set; }
        public List<SelectListItem> Warehouses { get; set; }
        public List<SelectListItem> Units { get; set; }
        public DateTime Now = DateTime.Now;

        public async Task OnGet()
        {
            var response = await Api.GetApi("Client");
            var result = await response.Content.ReadAsStringAsync();
            var clients = JsonConvert.DeserializeObject<List<ListClients>>(result);

            Clients = clients.Select(a =>
                new SelectListItem
                {
                    Value = a.Nif.ToString(),
                    Text =  a.Nif.ToString() + " - " + a.Name
                }).ToList();

            response = await Api.GetApi("Warehouse");
            result = await response.Content.ReadAsStringAsync();
            var warehouses = JsonConvert.DeserializeObject<List<ListWarehouses>>(result);

            Warehouses = warehouses.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text =  a.Name.ToString()
                }).ToList();


            response = await Api.GetApi("Stock");
            result = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ListProducts>>(result);

            Products = products.Select(a =>
                new SelectListItem
                {
                    Value = a.Reference.ToString(),
                    Text =  a.Description.ToString()
                }).ToList();

            IdsProducts = products.Select(a =>
                new SelectListItem
                {
                    Value = a.Reference.ToString(),
                    Text =  a.Reference.ToString()
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(ExitProducts, "Stock/Exit");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }
    }
}
