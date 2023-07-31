using System.Net;
using EM.DataRepository.Warehouses;
using EM.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EM.Web.Areas.Warehouse.Pages
{
    public class IndexModel : PageModel
    {
        public IList<ListWarehouses>? Warehouses { get; set; } = new List<ListWarehouses>();

        [BindProperty] public CreateWarehouse CreateWarehouse { get; set; } = new CreateWarehouse();

        [BindProperty] public CreateWarehouse EditWarehouse { get; set; } = new CreateWarehouse();

        public async Task<IActionResult> OnGet()
        {
            var response = await Api.GetApi("Warehouse");

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = await response.Content.ReadAsStringAsync();
                    Warehouses = JsonConvert.DeserializeObject<IList<ListWarehouses>>(result);
                    return Page();
                case HttpStatusCode.NoContent:
                    return Page();
                default:
                    return Page();
            }
        }

        public async Task<IActionResult> OnGetWarehouseInfoAsync(int id)
        {
            var response = await Api.GetApi("Warehouse/" + id);

            var result = await response.Content.ReadAsStringAsync();

            var warehouseDetails = JsonConvert.DeserializeObject<InfoWarehouse>(result);

            var resultJson = new
            {
                data1 = warehouseDetails?.Id,
                data2 = warehouseDetails?.Name
            };

            return new JsonResult(resultJson);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await Api.PostApi(CreateWarehouse, "Warehouse");

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
            var response = await Api.PutApi(EditWarehouse, "Warehouse");

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
            await Api.DeleteApi("Warehouse/" + id);
        }
    }
}