using EM.DataRepository.Clients;

namespace EM.Services;

public interface IClientService
{
    Task<List<ListClients>> ListClientTask();
    Task<InfoClient> InfoClientTask(string nif);
    Task CreateClientTask(CreateClient client);
    Task EditClientTask(CreateClient client);
    Task DisableClientTask(string nif);
}