using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.DataRepository.Families;
using EM.DataRepository.Products;
using EM.DataRepository.Suppliers;
using EM.DataRepository.TaxesProduct;
using EM.DataRepository.Types;
using EM.DataRepository.Units;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using EM.DataRepository.Stock;

namespace EM.Web.Areas.Stock.Pages
{
    public class IndexModel : PageModel
	{

		public IList<ListProducts>? Products { get; set; } = new List<ListProducts>();
		public List<SelectListItem> IdSuppliers { get; set; }
		public List<SelectListItem> IdTax { get; set; }
		public List<SelectListItem> IdTypeProducts { get; set; }
		public List<SelectListItem> IdUnits { get; set; }
		public List<SelectListItem> IdFamilies { get; set; }
		[BindProperty] public CreateProduct CreateProduct { get; set; } = new CreateProduct();

		[BindProperty] public CreateProduct EditProduct { get; set; } = new CreateProduct();

		public async Task<IActionResult> OnGet()
		{
			var response = await Api.GetApi("Supplier");
			var result = await response.Content.ReadAsStringAsync();
			var suppliers = JsonConvert.DeserializeObject<List<ListSuppliers>>(result);

			IdSuppliers = suppliers.Select(a =>
			new SelectListItem
			{
				Value = a.Nif.ToString(),
				Text = a.Nif.ToString() + " - " + a.Name
			}).ToList();

			response = await Api.GetApi("TaxProduct");
			result = await response.Content.ReadAsStringAsync();
			var taxes = JsonConvert.DeserializeObject<List<ListTaxesProducts>>(result);

			IdTax = taxes.Select(a =>
			new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name.ToString()
			}).ToList();

			response = await Api.GetApi("Type");
			result = await response.Content.ReadAsStringAsync();
			var typeProducts = JsonConvert.DeserializeObject<List<ListTypes>>(result);

			IdTypeProducts = typeProducts.Select(a =>
			new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name.ToString()
			}).ToList();

			response = await Api.GetApi("Unit");
			result = await response.Content.ReadAsStringAsync();
			var units = JsonConvert.DeserializeObject<List<ListUnits>>(result);

			IdUnits = units.Select(a =>
			new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Type.ToString()
			}).ToList();

			response = await Api.GetApi("Family");
			result = await response.Content.ReadAsStringAsync();
			var families = JsonConvert.DeserializeObject<List<ListFamilies>>(result);

			IdFamilies = families.Select(a =>
			new SelectListItem
			{
				Value = a.Id.ToString(),
				Text = a.Name.ToString()
			}).ToList();

			response = await Api.GetApi("Product");

			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:
					result = await response.Content.ReadAsStringAsync();
					Products = JsonConvert.DeserializeObject<IList<ListProducts>>(result);
					return Page();
				case HttpStatusCode.NoContent:
					return Page();
				default:
					return Page();
			}
		}
		public async Task<IActionResult> OnPostAsync()
		{
			var response = await Api.PostApi(CreateProduct, "Product");

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
