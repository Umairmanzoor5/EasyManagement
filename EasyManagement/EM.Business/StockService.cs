using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Products;
using EM.DataRepository.Stock;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class StockService : IStockService
{
    private readonly ApplicationDbContext _context;

    public StockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task EntryProductsTask(Entry products)
    {
        var movement = new SupplierClientMovement
        {
            Date = products.Buy,
            IdSupplier = products.Supplier,
            IsEntry = true,
            IdWarehouse = products.Warehouse
        };

        var x = await _context.SupplierClientMovements.AddAsync(movement);

        await _context.SaveChangesAsync();

        foreach (var entryProducts in products.Products.Select(product => new ProductsMovement
                 {
                     IdMovement = x.Entity.Id,
                     Quantity = product.Quantity,
                     Price = (decimal) product.Price,
                     RefProduct = product.Reference
                 }))
        {
            await _context.ProductsMovements.AddAsync(entryProducts);
        }

        await _context.SaveChangesAsync();
    }

    public async Task AdjustmentProductsTask(AdjustStock adjustStock)
    {
        var movement = new SupplierClientMovement
        {
            Date = DateTime.Now,
            IsEntry = false,
            IdWarehouse = adjustStock.Warehouse
        };

        var x = await _context.SupplierClientMovements.AddAsync(movement);

        await _context.SaveChangesAsync();

        foreach (var entryProducts in adjustStock.Products.Select(product => new ProductsMovement
                 {
                     IdMovement = x.Entity.Id,
                     Quantity = product.OldQuantity - product.NewQuantity,
                     Price = 0,
                     RefProduct = product.Reference
                 }))
        {
            await _context.ProductsMovements.AddAsync(entryProducts);
        }

        await _context.SaveChangesAsync();
    }

    public async Task ExitProductsTask(Exit products)
    {
        var movement = new SupplierClientMovement
        {
            Date = products.Sell,
            IdClient = products.Client,
            IsEntry = false,
            IdWarehouse = products.Warehouse
        };

        var x = await _context.SupplierClientMovements.AddAsync(movement);

        await _context.SaveChangesAsync();

        foreach (var exitProducts in products.Products.Select(product => new ProductsMovement
                 {
                     IdMovement = x.Entity.Id,
                     Quantity = product.Quantity,
                     Price = (decimal)product.Price,
                     RefProduct = product.Reference
                 }))
        {
            await _context.ProductsMovements.AddAsync(exitProducts);
        }

        await _context.SaveChangesAsync();
    }

    public async Task TransferProductsTask(Transfer transfer)
	{
		var movementExit = new SupplierClientMovement
		{
			Date = DateTime.Now,
			IsEntry = false,
			IdWarehouse = transfer.WarehouseExit
		};

		var movementEntry = new SupplierClientMovement
		{
			Date = DateTime.Now,
			IsEntry = true,
			IdWarehouse = transfer.WarehouseEntry
		};

		var x = await _context.SupplierClientMovements.AddAsync(movementExit);
		var y = await _context.SupplierClientMovements.AddAsync(movementEntry);

		await _context.SaveChangesAsync();


        foreach  (var product in transfer.Products) {
            var productTr = new ProductsMovement
            {
                IdMovement = x.Entity.Id,
                Quantity = product.Quantity,
                Price = 0,
                RefProduct = product.Reference
            };
			await _context.ProductsMovements.AddAsync(productTr);
            productTr.IdMovement = y.Entity.Id;
			await _context.ProductsMovements.AddAsync(productTr);
		}
		await _context.SaveChangesAsync();
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

    public async Task<List<ListProductsWarehouse>> ListProductsWarehouseTask(int id)
    {
        var listProduct = await _context.ViewStockWarehouses.Where(x=>x.Warehouse == id || x.Warehouse == null).Select(product => new ListProductsWarehouse
        {
           Warehouse = product.Warehouse,
           Reference = product.Reference,
           Description = product.Description,
           StockNow = product.StockNow
        }).ToListAsync();

        return listProduct;
    }

    public async Task<List<string>> VerifyStock(string products)
    {
        string[] products_array = products.Split(new string[] { "," }, StringSplitOptions.None); // Products to verify if exist in stock.
		List<string> products_list = products_array.OfType<string>().ToList(); // Products to verify if exist in stock.

		List<string> products_in_stock = new List<string>(); // Products in stock with quantity >= 1.
        for (int i = 0; i < products_list.Count; i++)
        { 
            await _context.ViewCurrentStocks.SingleAsync(x => x.Reference == products_list[i] && x.Quantity >= 1);
            products_in_stock.Add(products_list[i]);
        }        

        return products_in_stock;


    }

    //public async Task CreateProductTask(CreateProduct product)
    //{
    //	var newProduct = new Product
    //	{
    //		Reference = product.Reference,
    //		Description = product.Description,
    //		IdFamily = product.IdFamily,
    //		IdUnit = product.IdUnit,
    //		Barcode = product.Barcode,
    //		IdTypeProduct = product.IdTypeProduct,
    //		StockControl = product.StockControl,
    //		StockAlert = product.StockAlert,
    //		IdTax = product.IdTax,
    //		Sale = product.Sale,
    //		Purchase = product.Purchase,
    //		IdSuppliers = product.IdSuppliers
    //	};

    //	await _context.Products.AddAsync(newProduct);
    //	await _context.SaveChangesAsync();
    //}
}