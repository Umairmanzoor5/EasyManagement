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

namespace EM.Web.Areas.Budget.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty] public CreateBudget Budget { get; set; } = new CreateBudget();
		public List<SelectListItem> IdsProducts { get; set; }
		public List<SelectListItem> Products { get; set; }		
		public List<SelectListItem> Units { get; set; }


		public async Task OnGet(string? id)
		{
            var response = await Api.GetApi("Stock");
            var result = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ListProducts>>(result);


            if (id != null)
            {
                Budget.IdProject = id;

                response = await Api.GetApi("Project/Info/" + id);
                result = await response.Content.ReadAsStringAsync();
                var project = JsonConvert.DeserializeObject<InfoProject>(result);

                if (project != null)
                {
					Budget.Client = project.Client;
                    Budget.Description = project.Description;
					Budget.CreateDate = project.CreateDate;
					Budget.UpdateDate = DateTime.Now;
                }
            }

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
        }
		

		/* From all the products selected get those that are in stock */
		public async Task<IActionResult> OnGetPriceProductAsync(string id)
        {
            var response = await Api.GetApi("Stock/");

            var result = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ListProducts>>(result);

            var prod = products.FirstOrDefault(x => x.Reference == id);

            if (prod is null)
                return null;

            var resultJson = new
            {
                data = prod.Pvp
            };

            return new JsonResult(resultJson);
        }

		public async Task<IActionResult> OnPostAsync()
		{
			var response = await Api.PostApi(Budget, "Budget");

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
