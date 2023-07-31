using EM.Business;
using EM.DataRepository.Families;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class FamilyController : ControllerBase
{
    private readonly IFamilyService _familyService;

    public FamilyController(IFamilyService familyService)
    {
		_familyService = familyService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _familyService.ListFamilyTask());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    // GET api/<FamilyController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await _familyService.InfoFamilyTask(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(id);
        }
    }

    // POST api/<EntitiesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateFamily family)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            await _familyService.CreateFamilyTask(family);

            return Created(string.Empty, family);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("Family Conflict - " + family.Name);
        }

    }

    /// PUT api/<FamilyController>/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateFamily family)
    {
        try
        {
            await _familyService.EditFamilyTask(family);
            return Ok(family);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(family.Id);
        }
    }

    // DELETE api/<EntitiesController>/5
    //[HttpDelete("{nif}")]
    //public async Task<IActionResult> Delete(string nif)
    //{
    //    try
    //    {
    //        await _clientService.DisableClientTask(nif);

    //        return Ok();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //        return NotFound(nif);
    //    }
    //}
}