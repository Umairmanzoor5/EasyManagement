using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Warehouses;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _context;

    public WarehouseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListWarehouses>> ListWarehouseTask()
    {
        var listWarehouse = await _context.Warehouses.Select(warehouse => new ListWarehouses
            {
                Id = warehouse.Id,
                Name = warehouse.Name
            })
        .ToListAsync();

        return listWarehouse;
    }

    public async Task<InfoWarehouse> InfoWarehouseTask(int id)
    {
        var warehouse = await _context.Warehouses.SingleAsync(x=>x.Id == id);

        var infoWarehouse = new InfoWarehouse
        {
            Id = warehouse.Id,
            Name = warehouse.Name
        };

        return infoWarehouse;
    }

    public async Task CreateWarehouseTask(CreateWarehouse warehouse)
    {
        var newWarehouse = new Warehouse
        {
            Id = warehouse.Id,
            Name = warehouse.Name
        };

        await _context.Warehouses.AddAsync(newWarehouse);
        await _context.SaveChangesAsync();
    }

    public async Task EditWarehouseTask(CreateWarehouse warehouse)
    {
        Warehouse result;
        try
        {
            result = await _context.Warehouses.SingleAsync(x => x.Id == warehouse.Id);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        result.Name = warehouse.Name;
        
        
        await _context.SaveChangesAsync();
    }

    
    public async Task DisableWarehouseTask(int id)
    {
        var result = await _context.Warehouses.SingleAsync(x => x.Id == id);

        result.Active = false;

        await _context.SaveChangesAsync();
    }
    

    /*
    private List<ListBuyWarehouse> listBuyWarehouse(int id)
    {
        return null;
    }
    */

    /*
    private List<ListProjectsWarehouse> listProjectsWarehouse(int id)
    {
        return null;
    }
    */
}