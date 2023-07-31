using EM.DatabaseSQL.Tables;
using EM.DataRepository.Products;
using EM.DataRepository.Projects;

namespace EM.Services;

public interface IProjectService
{
	Task<List<ListProjects>> ListProjectTask();
	Task CreateProjectTask(CreateProject project);
	Task<InfoProject> InfoProjectTask(string id);
	Task RecuseProjectTask(string id);
	Task ApproveProjectTask(string id);
	Task UpdateDateTask(string id);
	Task ChangeStateTask(string id, int state);
	//Task EditProductTask(CreateProduct product);
	//Task DisableProductTask(string reference);
	//Task<List<Product>> GetProductsByFamilyTask(int familyId);
 //   Task<List<Product>> GetProductsByTaxTask(int taxId);
 //   Task<List<Product>> GetProductsByUnitTask(int unitId);
 //   Task<List<Product>> GetProductsByTypesProductTask(int typesProductId);

}