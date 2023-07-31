using EM.Business;
using EM.DataRepository.Units;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _unitService.ListUnitTask());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

       
        // GET api/<UnitController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitService.InfoUnitTask(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(id);
            }
        }

        // POST api/<UnitController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUnit unit)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _unitService.CreateUnitTask(unit);

                return Created(string.Empty, unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Conflict("Unit Conflict");
            }

        }

        /// PUT api/<UnitController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CreateUnit unit)
        {
            try
            {
                await _unitService.EditUnitTask(unit);
                return Ok(unit);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound(unit.Id);
            }
        }

        // DELETE api/<UnitController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
