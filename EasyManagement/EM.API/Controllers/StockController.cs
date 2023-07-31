using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;
        private readonly IProductService _productService;

        public StockController(IStockService stockService, IProductService productService)
        {
            _stockService = stockService;
            _productService = productService;
        }


        [HttpGet]
        public async Task<List<ListProducts>> Get()
        {
            return await _stockService.ListProductTask();
        }
        
        [HttpGet("ProductsWarehouse/{idWarehouse}")]
        public async Task<List<ListProductsWarehouse>> GetProductsWarehouse(int idWarehouse)
        {
            return await _stockService.ListProductsWarehouseTask(idWarehouse);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [Route("Entry")]
        [HttpPost]
        public async Task<IActionResult> PostEntry([FromBody] Entry products)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _stockService.EntryProductsTask(products);

                return Created(string.Empty, products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("Products Conflict");
            }

        }

        [Route("Exit")]
        [HttpPost]
        public async Task<IActionResult> PostExit([FromBody] Exit products)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _stockService.ExitProductsTask(products);

                return Created(string.Empty, products);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("Products Conflict");
            }

        }

        [Route("Adjustment")]
        [HttpPost]
        public async Task<IActionResult> PostAdjustment([FromBody] AdjustStock adjustStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _stockService.AdjustmentProductsTask(adjustStock);

                return Created(string.Empty, adjustStock);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("Products Conflict");
            }

        }
        
		[Route("Transfers")]
		[HttpPost]
		public async Task<IActionResult> PostTransfers([FromBody] Transfer transfer)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				await _stockService.TransferProductsTask(transfer);

				return Created(string.Empty, transfer);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Conflict("Products Conflict");
			}

		}

        [HttpGet("VerifyStock/{products}")]
        public async Task<IActionResult> GetVerifyStock(string products)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {                
                return Ok(await _stockService.VerifyStock(products));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("None of this products are available in stock!");
            }

        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
