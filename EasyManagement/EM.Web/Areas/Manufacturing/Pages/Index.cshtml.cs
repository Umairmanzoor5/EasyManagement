using EM.DataRepository.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EM.Web.Areas.Manufacturing.Pages
{
    public class IndexModel : PageModel
    {
        public IList<ListProducts>? Products { get; set; } = new List<ListProducts>();

        public void OnGet()
        {
        }
    }
}
