using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Families;
using EM.DataRepository.TaxesProduct;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class FamilyService : IFamilyService
{
    private readonly ApplicationDbContext _context;

    public FamilyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListFamilies>> ListFamilyTask()
    {
        var listFamilies = await _context.FamiliesProducts.Select(family => new ListFamilies
            {
                Name = family.Name,
                Id = family.Id

		})
            .ToListAsync();

        return listFamilies;
    }
    public async Task<InfoFamily> InfoFamilyTask(int id)
    {
        var family = await _context.FamiliesProducts.SingleAsync(x => x.Id == id);

        var infoFamily = new InfoFamily
        {
            Id = family.Id,
            Name = family.Name
        };

        return infoFamily;
    }
    public async Task CreateFamilyTask(CreateFamily family)
    {
        var newFamily = new FamiliesProduct
        {
            Name = family.Name,
        };

        await _context.FamiliesProducts.AddAsync(newFamily);
        await _context.SaveChangesAsync();
    }

    public async Task EditFamilyTask(CreateFamily family)
    {
        var result = await _context.FamiliesProducts.SingleAsync(x => x.Id == family.Id);
        result.Name = family.Name;
        
        await _context.SaveChangesAsync();
    }
}