using EM.DatabaseSQL.Tables;
using EM.DataRepository.Units;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace EM.Web.Areas.Unit.Pages
{
    public class IndexModel : PageModel
	{

	    public IList<ListUnits>? Units { get; set; } = new List<ListUnits>();

        [BindProperty] public CreateUnit CreateUnit { get; set; } = new CreateUnit();

        [BindProperty] public CreateUnit EditUnit { get; set; } = new CreateUnit();

        public async Task<IActionResult> OnGet()
		{
			var response = await Api.GetApi("Unit");
			
			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					var result = await response.Content.ReadAsStringAsync();
                    Units = JsonConvert.DeserializeObject<IList<ListUnits>>(result);
					return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
		}
        public async Task<IActionResult> OnGetUnitInfoAsync(int id)
        {
            var response = await Api.GetApi("Unit/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var unitDetails = JsonConvert.DeserializeObject<InfoUnit>(result);

            var resultJson = new
            {
                data1 = unitDetails?.Id,
                data2 = unitDetails?.Type

            };

            return new JsonResult(resultJson);
        }

		/* Get all products with requested unit's id */
		public async Task<IActionResult> OnGetProductsByUnitAsync(int id)
		{
			var response = await Api.GetApi("Product/ByUnit/" + id);

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
            var response = await Api.PostApi(CreateUnit, "Unit");

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
            var response = await Api.PutApi(EditUnit, "Unit");

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
            await Api.DeleteApi("Unit/" + id);
        }
    }
}
