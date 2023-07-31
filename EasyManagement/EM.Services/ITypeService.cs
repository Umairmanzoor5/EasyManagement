using EM.DataRepository.Types;

namespace EM.Services;

public interface ITypeService
{
    Task<List<ListTypes>> ListTypeTask();
    Task CreateTypeTask(CreateType type);
    Task<InfoType> InfoTypeTask(int id);
    Task EditTypeTask(CreateType type);
    Task DisableTypeTask(int id);
}