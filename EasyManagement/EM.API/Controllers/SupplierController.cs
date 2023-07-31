using EM.DataRepository.Suppliers;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{

		private readonly ISupplierService _supplierService;

		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(await _supplierService.ListSupplierTask());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}
		}

		[HttpGet("{nif}")]
		public async Task<IActionResult> Get(string nif)
		{
			try
			{
				return Ok(await _supplierService.InfoSupplierTask(nif));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(nif);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateSupplier supplier)
		{
			try
			{
				await _supplierService.CreateSupplierTask(supplier);
				return Created(string.Empty, supplier);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Conflict("VAT Number Conflict - " + supplier.Nif);
			}

		}


		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CreateSupplier supplier)
		{
			try
			{
				await _supplierService.EditSupplierTask(supplier);
				return Ok(supplier);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(supplier.Nif);
			}
		}

		[HttpDelete("{nif}")]
		public async Task<IActionResult> Delete(string nif)
		{
			try
			{
				await _supplierService.DisableSupplierTask(nif);
				return Ok();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(nif);
			}
		}
	}
}
