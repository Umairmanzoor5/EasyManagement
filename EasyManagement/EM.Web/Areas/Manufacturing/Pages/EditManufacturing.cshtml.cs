using System.Net;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.DataRepository.Warehouses;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EM.Web.Areas.Manufacturing.Pages
{
    public class EditManufacturingModel : PageModel
    {
        [BindProperty] public AdjustStock AdjustStock { get; set; }
        public IList<ListProducts>? Products { get; set; } = new List<ListProducts>();

        public List<SelectListItem> Warehouses { get; set; }
        public DateTime Now = DateTime.Now;

        public async Task OnGet()
        {
            var response = await Api.GetApi("Warehouse");
            var result = await response.Content.ReadAsStringAsync();
            var warehouses = JsonConvert.DeserializeObject<List<ListWarehouses>>(result);

            if (warehouses != null)
                Warehouses = warehouses.Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name.ToString()
                    }).ToList();

            response = await Api.GetApi("Product");
            result = await response.Content.ReadAsStringAsync();
            Products = JsonConvert.DeserializeObject<IList<ListProducts>>(result);
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    return Page();
        //}

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
