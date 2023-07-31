using EM.DataRepository.Families;

namespace EM.Services;

public interface IFamilyService
{
    Task<List<ListFamilies>> ListFamilyTask();
    Task<InfoFamily> InfoFamilyTask(int id);
    Task CreateFamilyTask(CreateFamily family);
    Task EditFamilyTask(CreateFamily family);
}