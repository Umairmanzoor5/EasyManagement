using EM.DatabaseSQL.Tables;
using EM.DataRepository.Types;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace EM.Web.Areas.TypesProduct.Pages
{
    public class IndexModel : PageModel
	{

	    public IList<ListTypes>? TypesProduct { get; set; } = new List<ListTypes>();

        [BindProperty] public CreateType CreateTypeProduct { get; set; } = new CreateType();

        [BindProperty] public CreateType EditTypeProduct { get; set; } = new CreateType();

        public async Task<IActionResult> OnGet()
		{
			var response = await Api.GetApi("Type");
			
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					var result = await response.Content.ReadAsStringAsync();
					TypesProduct = JsonConvert.DeserializeObject<IList<ListTypes>>(result);
					return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
		}
        public async Task<IActionResult> OnGetTypesProductInfoAsync(int id)
        {
            var response = await Api.GetApi("Type/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var typeProductDetails = JsonConvert.DeserializeObject<InfoType>(result);

            var resultJson = new
            {
                data1 = typeProductDetails?.Id,
                data2 = typeProductDetails?.Name
                
            };

            return new JsonResult(resultJson);
        }

		/* Get all products with requested typeProduct's id */
		public async Task<IActionResult> OnGetProductsByTypesProductAsync(int id)
		{
			var response = await Api.GetApi("Product/ByTypesProduct/" + id);

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
            var response = await Api.PostApi(CreateTypeProduct, "Type");

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
            var response = await Api.PutApi(EditTypeProduct, "Type");

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
            await Api.DeleteApi("Type/" + id);
        }
    }
}
