using EM.DataRepository.Units;

namespace EM.Services;

public interface IUnitService
{
    Task<List<ListUnits>> ListUnitTask();
    Task CreateUnitTask(CreateUnit unit);
    Task<InfoUnit> InfoUnitTask(int id);
    Task EditUnitTask(CreateUnit unit);
    Task DisableUnitTask(int id);
    
}