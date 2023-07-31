using EM.DataRepository.Warehouses;

namespace EM.Services;

public interface IWarehouseService
{
    Task<List<ListWarehouses>> ListWarehouseTask();
    Task<InfoWarehouse> InfoWarehouseTask(int id);
    Task CreateWarehouseTask(CreateWarehouse warehouse);
    Task EditWarehouseTask(CreateWarehouse warehouse);
    Task DisableWarehouseTask(int id);
}