using System.Net;
using System.Net.Http.Headers;
using EM.DataRepository.Suppliers;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EM.Web.Areas.Supplier.Pages
{
    public class IndexModel : PageModel
    {
        public IList<ListSuppliers>? Suppliers { get; set; } = new List<ListSuppliers>();

		[BindProperty] public CreateSupplier CreateSupplier { get; set; } = new();

		[BindProperty] public CreateSupplier EditSupplier { get; set; } = new();


		public async Task<IActionResult> OnGet()
        {
            var response = await Api.GetApi("Supplier");

			switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    Suppliers = JsonConvert.DeserializeObject<IList<ListSuppliers>>(result);
                    return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
        }

		public async Task<IActionResult> OnGetSupplierInfoAsync(string nif)
		{
			var response = await Api.GetApi("Supplier/" + nif);

			var result = await response.Content.ReadAsStringAsync();

			var supplierDetails = JsonConvert.DeserializeObject<InfoSupplier>(result);

			var resultJson = new
			{
				data1 = supplierDetails?.Nif,
				data2 = supplierDetails?.Name,
				data3 = supplierDetails?.Address1,
				data4 = supplierDetails?.Address2,
				data5 = supplierDetails?.PostalCode,
				data6 = supplierDetails?.City,
				data7 = supplierDetails?.NameComercial,
				data8 = supplierDetails?.ContactComercial,
				data9 = supplierDetails?.EmailComercial
			};

			return new JsonResult(resultJson);
		}

		public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateSupplier, "Supplier");

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
			var response = await Api.PutApi(EditSupplier, "Supplier");

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
			await Api.DeleteApi("Supplier/" + nif);
		}

	}
}
