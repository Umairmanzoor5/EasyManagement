using EM.DatabaseSQL.Tables;
using EM.DataRepository.Families;
using EM.DataRepository.Suppliers;
using EM.DataRepository.Products;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace EM.Web.Areas.Family.Pages
{
    public class IndexModel : PageModel
	{

	    public IList<ListFamilies>? Families { get; set; } = new List<ListFamilies>();

        [BindProperty] public CreateFamily CreateFamily { get; set; } = new CreateFamily();

        [BindProperty] public CreateFamily EditFamily { get; set; } = new CreateFamily();

        public async Task<IActionResult> OnGet()
		{
			var response = await Api.GetApi("Family");
			
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					var result = await response.Content.ReadAsStringAsync();
                    Families = JsonConvert.DeserializeObject<IList<ListFamilies>>(result);
					return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
		}
        public async Task<IActionResult> OnGetFamilyInfoAsync(int id)
        {
            var response = await Api.GetApi("Family/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var familyDetails = JsonConvert.DeserializeObject<InfoFamily>(result);

            var resultJson = new
            {
                data1 = familyDetails?.Id,
                data2 = familyDetails?.Name
            };

            return new JsonResult(resultJson);
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
                List<string> details_list = new (details);
                resultJson.data.Add(details_list);
            }

            return new JsonResult(resultJson);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateFamily, "Family");

            return response.StatusCode switch
            {
                HttpStatusCode.Created => RedirectToPage(),
                _ => Page(),
            };
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var response = await Api.PutApi(EditFamily, "Family");

            return response.StatusCode switch
            {
                HttpStatusCode.OK => RedirectToPage(),
                _ => Page(),
            };
        }

        public async Task OnGetDeleteAsync(int id)
        {
            await Api.DeleteApi("Family/" + id);
        }
    }
}
