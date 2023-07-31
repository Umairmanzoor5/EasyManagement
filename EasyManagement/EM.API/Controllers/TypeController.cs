using EM.Business;
using EM.DataRepository.Types;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class TypeController : ControllerBase
{
    private readonly ITypeService _typeService;

	public TypeController(ITypeService typeService)
    {
		_typeService = typeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _typeService.ListTypeTask());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }


    // GET api/<TypeController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await _typeService.InfoTypeTask(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(id);
        }
    }

    // POST api/<TypeController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateType type)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            await _typeService.CreateTypeTask(type);

            return Created(string.Empty, type);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("Type Product Conflict - " + type.Name);
        }

    }

    /// PUT api/<TypeController>/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateType type)
    {
        try
        {
            await _typeService.EditTypeTask(type);
            return Ok(type);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(type.Id);
        }
    }

    // DELETE api/<TypeController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

}