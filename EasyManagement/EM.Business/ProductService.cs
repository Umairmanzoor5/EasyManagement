using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.DataRepository.Suppliers;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    
    public async Task<List<ListProducts>> ListProductTask()
    {
        var listProduct = await _context.ViewCurrentStocks.Select(product => new ListProducts
        {
            Reference = product.Reference,
            Description = product.Description,
            NameFamily = product.NameFamily,
            Pl = product.Pl,
            Pvp = product.Pvp,
            Quantity = product.Quantity,
            Unit = product.Unit
        }).ToListAsync();

        return listProduct;
    }

	public async Task CreateProductTask(CreateProduct product)
	{
		var newProduct = new Product
		{
			Reference = product.Reference,
			Description = product.Description,
			IdFamily = product.IdFamily,
			IdUnit = product.IdUnit,
			Barcode = product.Barcode,
			IdTypeProduct = product.IdTypeProduct,
			StockControl = product.StockControl,
			StockAlert = product.StockAlert,
			IdTax = product.IdTax,
			Sale = product.Sale,
			Purchase = product.Purchase,
			IdSuppliers = product.IdSuppliers
		};

		await _context.Products.AddAsync(newProduct);
		await _context.SaveChangesAsync();
	}

	public Task EditProductTask(CreateProduct product)
	{
		throw new NotImplementedException();
	}

	public Task DisableProductTask(string reference)
	{
		throw new NotImplementedException();
	}

    public async Task<List<Product>> GetProductsByFamilyTask(int familyId)
	{
		var products = await _context.Products.Where(p => p.IdFamily.Equals(familyId)).ToListAsync();

		return products;
	}

    public async Task<List<Product>> GetProductsByTaxTask(int taxId)
    {
        var products = await _context.Products.Where(p => p.IdTax.Equals(taxId)).ToListAsync();

        return products;
    }

    public async Task<List<Product>> GetProductsByUnitTask(int unitId)
    {
        var products = await _context.Products.Where(p => p.IdUnit.Equals(unitId)).ToListAsync();

        return products;
    }    

    public async Task<List<Product>> GetProductsByTypesProductTask(int typesProductId)
    {
        var products = await _context.Products.Where(p => p.IdTypeProduct.Equals(typesProductId)).ToListAsync();

        return products;
    }
}