using EM.DataRepository.Products;
using EM.DataRepository.Stock;

namespace EM.Services;

public interface IStockService
{
	//Task CreateProductTask(CreateProduct product);
    
	Task<List<ListProducts>> ListProductTask();
	Task<List<ListProductsWarehouse>> ListProductsWarehouseTask(int id);
    Task EntryProductsTask(Entry products);
    Task ExitProductsTask(Exit products);
    Task AdjustmentProductsTask(AdjustStock adjustStock);
    Task TransferProductsTask(Transfer transfer);
    Task<List<string>> VerifyStock(string products);
}