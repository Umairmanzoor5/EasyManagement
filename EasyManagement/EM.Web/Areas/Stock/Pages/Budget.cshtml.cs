using System.Net;
using EM.DataRepository.Budgets;
using EM.DataRepository.Products;
using EM.DataRepository.Projects;
using EM.DataRepository.Suppliers;
using EM.DataRepository.Warehouses;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EM.Web.Areas.Stock.Pages
{
    public class BudgetModel : PageModel
    {
		public InfoBudget Budget { get; set; }
		[BindProperty] public List<ListProjects> AddProductsBudget { get; set; }
		public List<SelectListItem> Suppliers { get; set; }
		public List<SelectListItem> IdsProducts { get; set; }
		public List<SelectListItem> Products { get; set; }		
		public List<SelectListItem> Warehouses { get; set; }
		public List<SelectListItem> Units { get; set; }

		//public DateTime Now = DateTime.Now;

		public async Task OnGet()
		{
			var response = await Api.GetApi("Supplier");
			var result = await response.Content.ReadAsStringAsync();
			var suppliers = JsonConvert.DeserializeObject<List<ListSuppliers>>(result);

			Suppliers = suppliers.Select(a =>
				new SelectListItem
				{
					Value = a.Nif.ToString(),
					Text = a.Nif.ToString() + " - " + a.Name
				}).ToList();

			response = await Api.GetApi("Warehouse");
			result = await response.Content.ReadAsStringAsync();
			var warehouses = JsonConvert.DeserializeObject<List<ListWarehouses>>(result);

			Warehouses = warehouses.Select(a =>
				new SelectListItem
				{
					Value = a.Id.ToString(),
					Text = a.Name.ToString()
				}).ToList();


			response = await Api.GetApi("Stock");
			result = await response.Content.ReadAsStringAsync();
			var products = JsonConvert.DeserializeObject<List<ListProducts>>(result);

			Products = products.Select(a =>
				new SelectListItem
				{
					Value = a.Reference.ToString(),
					Text = a.Description.ToString()
				}).ToList();

			IdsProducts = products.Select(a =>
				new SelectListItem
				{
					Value = a.Reference.ToString(),
					Text = a.Reference.ToString()
				}).ToList();

			response = await Api.GetApi("Budget");
			result = await response.Content.ReadAsStringAsync();
			var budgets = JsonConvert.DeserializeObject<List<InfoBudget>>(result);

			Budget = budgets[0];

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

		public async Task<IActionResult> OnPostAsync()
		{
			var response = await Api.PostApi(AddProductsBudget, "Stock/Entry");

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
