using EM.Business;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.DataRepository.TaxesProduct;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaxProductController : ControllerBase
    {
        private readonly ITaxProductService _taxProductService;

        public TaxProductController(ITaxProductService taxProductService)
        {
            _taxProductService = taxProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _taxProductService.ListTaxTask());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // GET api/<TaxProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _taxProductService.InfoTaxTask(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(id);
            }
        }

        // POST api/<TaxProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTax tax)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _taxProductService.CreateTaxTask(tax);

                return Created(string.Empty, tax);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("Tax Conflict");
            }

        }

        /// PUT api/<TaxProductController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateTax tax)
        {
            try
            {
                await _taxProductService.EditTaxTask(tax);
                return Ok(tax);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(tax.Id);
            }
        }

        // DELETE api/<TaxProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
