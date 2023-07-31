using EM.DataRepository.Warehouses;
using EM.Services;
using Microsoft.AspNetCore.Mvc;

namespace EM.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _warehouseService.ListWarehouseTask());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            return Ok(await _warehouseService.InfoWarehouseTask(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(id);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateWarehouse warehouse)
    {
        try
        {
            await _warehouseService.CreateWarehouseTask(warehouse);

            return Created(string.Empty, warehouse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict("VAT Number Conflict - " + warehouse.Id);
        }

    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateWarehouse warehouse)
    {
        try
        {
            await _warehouseService.EditWarehouseTask(warehouse);
            return Ok(warehouse);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(warehouse.Id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _warehouseService.DisableWarehouseTask(id);

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(id);
        }
    }
}