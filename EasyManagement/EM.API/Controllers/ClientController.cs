using EM.DataRepository.Clients;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _clientService.ListClientTask());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    // GET api/<EntitiesController>/5
    [HttpGet("{nif}")]
    public async Task<IActionResult> Get(string nif)
    {
        try
        {
            return Ok(await _clientService.InfoClientTask(nif));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(nif);
        }
    }

    // POST api/<EntitiesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateClient client)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            await _clientService.CreateClientTask(client);

            return Created(string.Empty, client);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("VAT Number Conflict - " + client.Nif);
        }

    }

    // PUT api/<EntitiesController>/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateClient client)
    {
        try
        {
            await _clientService.EditClientTask(client);
            return Ok(client);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(client.Nif);
        }
    }

    // DELETE api/<EntitiesController>/5
    [HttpDelete("{nif}")]
    public async Task<IActionResult> Delete(string nif)
    {
        try
        {
            await _clientService.DisableClientTask(nif);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(nif);
        }
    }
}