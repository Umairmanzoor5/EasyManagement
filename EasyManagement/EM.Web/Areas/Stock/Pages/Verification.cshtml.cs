using System.Linq;
using System.Net;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.DataRepository.Families;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using EM.DataRepository.Accounts;

namespace EM.Web.Areas.Stock.Pages
{
    public class VerificationModel : PageModel
    {
        public SelectList Families { get; set; }
        //[BindProperty] public List<string> StockToVerify { get; set; } = new List<string>();

        public void OnGet()
        {
            List<ListFamilies> families = PopulateFamilies().Result;
            IEnumerable<ListFamilies> items = families;
            Families = new SelectList(items, "Id", "Name");
        }
        public async Task<List<ListFamilies>> PopulateFamilies()
        {
            List<ListFamilies> families = new List<ListFamilies>();
            var response = await Api.GetApi("Family");            
            var result = await response.Content.ReadAsStringAsync();
            families = JsonConvert.DeserializeObject<List<ListFamilies>>(result);
            return families;                   
        }

        /* Get all products with requested family's id */
        public async Task<IActionResult> OnGetProductsByFamilyAsync(int id)
        {
            var response = await Api.GetApi("Product/ByFamily/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<Product>>(result);

            var resultJson = new
            {
                data = new List<List<string>>()
            };

            for (int i = 0; i < products.Count; i++)
            {
                string[] details = { products[i].Reference, products[i].Description };
                List<string> details_list = new(details);
                resultJson.data.Add(details_list);
            }

            return new JsonResult(resultJson);
        }

        /* From all the products selected get those that are in stock */
        public async Task<IActionResult> OnGetVerifyStockAsync(string products)
        {            
            var response = await Api.GetApi("Stock/VerifyStock/" + products);

            var result = await response.Content.ReadAsStringAsync();

            var products_in_stock = JsonConvert.DeserializeObject<List<string>>(result);

            var resultJson = new
            {
                data = products_in_stock
            };

            return new JsonResult(resultJson);
        }
    }
}
