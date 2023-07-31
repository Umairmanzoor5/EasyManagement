using System.Linq;
using System.Net;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.DataRepository.Suppliers;
using EM.DataRepository.Warehouses;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EM.Web.Areas.Stock.Pages
{
    public class TransfersModel : PageModel
    {
		//[BindProperty] public AdjustStock AdjustStock { get; set; }
		//[BindProperty] public Entry EntryProducts { get; set; }
		[BindProperty] public Transfer TransferStock { get; set; }
		//[BindProperty] public Exit ExitProducts { get; set; }



		public List<SelectListItem> Warehouses { get; set; }
        public DateTime Now = DateTime.Now;

        public async Task OnGet()
        {
            var response = await Api.GetApi("Warehouse");
            var result = await response.Content.ReadAsStringAsync();
            var warehouses = JsonConvert.DeserializeObject<List<ListWarehouses>>(result);

            Warehouses = warehouses.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text =  a.Name.ToString()
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(TransferStock, "Stock/Transfers");

            switch (response.StatusCode)
            {
                case HttpStatusCode.Created:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnGetProductsWarehouseAsync(int id)
        {
            var response = await Api.GetApi("Stock/ProductsWarehouse/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var clientDetails = new List<ListProductsWarehouse>();

            try
            {
                clientDetails = JsonConvert.DeserializeObject<List<ListProductsWarehouse>>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            var r = clientDetails.Select(a => new
            {
                a.Reference,
                a.Description,
                a.StockNow
            });


            return new JsonResult(r);
        }
    }
}
