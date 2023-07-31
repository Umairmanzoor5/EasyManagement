using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Types;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class TypeService : ITypeService
{
    private readonly ApplicationDbContext _context;

    public TypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListTypes>> ListTypeTask()
    {
        var listTypes = await _context.TypesProducts.Select(type => new ListTypes
		{
                Name = type.Name,
                Id = type.Id

		})
            .ToListAsync();

        return listTypes;
    }

    public async Task CreateTypeTask(CreateType type)
    {
        var newType = new TypesProduct
        {
            Name = type.Name,
        };

        await _context.TypesProducts.AddAsync(newType);
        await _context.SaveChangesAsync();
    }

    public async Task<InfoType> InfoTypeTask(int id)
    {
        var type = await _context.TypesProducts.SingleAsync(x => x.Id == id);

        var infoType = new InfoType
        {
            Id = type.Id,
            Name = type.Name
        };

        return infoType;
    }

    public async Task EditTypeTask(CreateType type)
    {
        var result = await _context.TypesProducts.SingleAsync(x => x.Id == type.Id);
        result.Name = type.Name;

        await _context.SaveChangesAsync();
    }

    public Task DisableTypeTask(int id)
    {
        throw new NotImplementedException();
    }
}