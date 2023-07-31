using EM.DatabaseSQL.Tables;
using EM.DataRepository.TaxesProduct;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace EM.Web.Areas.Tax.Pages
{
    public class IndexModel : PageModel
	{

	    public IList<ListTaxesProducts>? Taxes { get; set; } = new List<ListTaxesProducts>();

        [BindProperty] public CreateTax CreateTax { get; set; } = new CreateTax();

        [BindProperty] public CreateTax EditTax { get; set; } = new CreateTax();

        public async Task<IActionResult> OnGet()
		{
			var response = await Api.GetApi("TaxProduct");
			
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					var result = await response.Content.ReadAsStringAsync();
					Taxes = JsonConvert.DeserializeObject<IList<ListTaxesProducts>>(result);
					return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
		}
        public async Task<IActionResult> OnGetTaxInfoAsync(int id)
        {
            var response = await Api.GetApi("TaxProduct/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var taxDetails = JsonConvert.DeserializeObject<InfoTax>(result);

            var resultJson = new
            {
                data1 = taxDetails?.Id,
                data2 = taxDetails?.Name,
                data3 = taxDetails?.Tax
            };

            return new JsonResult(resultJson);
        }

		/* Get all products with requested tax's id */
		public async Task<IActionResult> OnGetProductsByTaxAsync(int id)
		{
			var response = await Api.GetApi("Product/ByTax/" + id);

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

		public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateTax, "TaxProduct");

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
            var response = await Api.PutApi(EditTax, "TaxProduct");

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return RedirectToPage();
                default:
                    return Page();
            }
        }

        public async Task OnGetDeleteAsync(int id)
        {
            await Api.DeleteApi("TaxProduct/" + id);
        }
    }
}
