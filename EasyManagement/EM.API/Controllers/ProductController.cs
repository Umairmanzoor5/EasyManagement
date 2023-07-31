using Microsoft.AspNetCore.Mvc;
using EM.DataRepository.Products;
using EM.Services;
using EM.Business;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
				return Ok(await _productService.ListProductTask());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}
		}

  //      // GET api/<ProductController>/5
  //      [HttpGet("{reference}")]
		//public async Task<IActionResult> Get(string nif)
		//{
		//	try
		//	{
		//		return Ok(await _productService.InfoProductTask(nif));
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		return NotFound(nif);
		//	}
		//}

		// POST api/<ProductController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateProduct product)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				await _productService.CreateProductTask(product);

				return Created(string.Empty, product);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return Conflict("VAT Number Conflict - " + product.Reference); //
			}

		}



		// PUT api/<ProductController>/5
		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CreateProduct product)
		{
			try
			{
				await _productService.EditProductTask(product);
				return Ok(product);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(product.Reference);
			}
		}

		// DELETE api/<ProductController>/5
		[HttpDelete("{reference}")]
		public async Task<IActionResult> Delete(string reference)
		{
			try
			{
				await _productService.DisableProductTask(reference);

				return Ok();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(reference);
			}
		}

        
        [HttpGet("ByFamily/{familyId}")]
        public async Task<IActionResult> GetByFamily(int familyId)
        {
        	try
        	{
        		return Ok(await _productService.GetProductsByFamilyTask(familyId));
        	}
        	catch (Exception e)
        	{
        		Console.WriteLine(e);
        		return NotFound(familyId);
        	}
        }

		[HttpGet("ByTax/{taxId}")]
		public async Task<IActionResult> GetByTax(int taxId)
		{
			try
			{
				return Ok(await _productService.GetProductsByTaxTask(taxId));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(taxId);
			}
		}

		[HttpGet("ByUnit/{unitId}")]
		public async Task<IActionResult> GetByUnit(int unitId)
		{
			try
			{
				return Ok(await _productService.GetProductsByUnitTask(unitId));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(unitId);
			}
		}

		[HttpGet("ByTypesProduct/{typesProductId}")]
		public async Task<IActionResult> GetByTypesProduct(int typesProductId)
		{
			try
			{
				return Ok(await _productService.GetProductsByTypesProductTask(typesProductId));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return NotFound(typesProductId);
			}
		}
	}
}
