using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Units;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class UnitService : IUnitService
{
    private readonly ApplicationDbContext _context;

    public UnitService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListUnits>> ListUnitTask()
    {
        var listUnits = await _context.UnitiesProducts.Select(unit => new ListUnits
            {
                Id = unit.Id,
                Type = unit.Type
            })
            .ToListAsync();

        return listUnits;
    }

    public async Task CreateUnitTask(CreateUnit unit)
    {
        var newUnit = new UnitiesProduct()
        {
            Type = unit.Type
        };

        await _context.UnitiesProducts.AddAsync(newUnit);
        await _context.SaveChangesAsync();
    }

    public async Task<InfoUnit> InfoUnitTask(int id)
    {
        var unit = await _context.UnitiesProducts.SingleAsync(x => x.Id == id);

        var infoUnit= new InfoUnit
        {
            Id = unit.Id,
            Type = unit.Type
        };

        return infoUnit;
    }

    public async Task EditUnitTask(CreateUnit unit)
    {
        var result = await _context.UnitiesProducts.SingleAsync(x => x.Id == unit.Id);
        result.Type = unit.Type;

        await _context.SaveChangesAsync();
    }

    public Task DisableUnitTask(int id)
    {
        throw new NotImplementedException();
    }
}