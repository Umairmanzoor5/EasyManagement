using EM.DatabaseSQL.Tables;
using EM.DataRepository.Products;

namespace EM.Services;

public interface IProductService
{
	Task<List<ListProducts>> ListProductTask();
	Task CreateProductTask(CreateProduct product);
	//Task<InfoProduct> InfoProductTask(string nif);
	Task EditProductTask(CreateProduct product);
	Task DisableProductTask(string reference);
	Task<List<Product>> GetProductsByFamilyTask(int familyId);
    Task<List<Product>> GetProductsByTaxTask(int taxId);
    Task<List<Product>> GetProductsByUnitTask(int unitId);
    Task<List<Product>> GetProductsByTypesProductTask(int typesProductId);

}