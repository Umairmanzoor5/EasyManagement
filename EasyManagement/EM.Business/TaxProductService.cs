using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.DataRepository.TaxesProduct;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class TaxProductService : ITaxProductService
{
    private readonly ApplicationDbContext _context;

    public TaxProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListTaxesProducts>> ListTaxTask()
    {
        var listTaxes = await _context.TaxesProducts.Select(tax => new ListTaxesProducts
        {
                Id = tax.Id,
                Name = tax.Name,
                Tax = tax.Tax
        })
            .ToListAsync();

        return listTaxes;
    }
    public async Task<InfoTax> InfoTaxTask(int id)
    {
        var tax = await _context.TaxesProducts.SingleAsync(x => x.Id == id);

        var infoTax = new InfoTax
        {
            Id = tax.Id,
            Name = tax.Name,
            Tax = tax.Tax
        };

        return infoTax;
    }
    public async Task CreateTaxTask(CreateTax tax)
    {
        var newTax = new TaxesProduct
        {
            Name = tax.Name,
            Tax = tax.Tax
        };

        await _context.TaxesProducts.AddAsync(newTax);
        await _context.SaveChangesAsync();
    }

    public async Task EditTaxTask(CreateTax tax)
    {
        var result = await _context.TaxesProducts.SingleAsync(x => x.Id == tax.Id);
        result.Name = tax.Name;
        result.Tax = tax.Tax;
        
        await _context.SaveChangesAsync();
    }

    public Task DisableTaxTask(int id)
    {
        throw new NotImplementedException();
    }
}