using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.Services;
using Microsoft.EntityFrameworkCore;

namespace EM.Business;

public class ClientService : IClientService
{
    private readonly ApplicationDbContext _context;

    public ClientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ListClients>> ListClientTask()
    {
        var listClient = await _context.Clients.Where(x=>x.Active == true).Select(client => new ListClients
            {
                Name = client.Name,
                Nif = client.Nif,
                ContactResponsible = client.ContactResponsible,
                EmailResponsible = client.EmailResponsible,
                NameResponsible = client.NameResponsible
            })
            .ToListAsync();

        return listClient;
    }

    public async Task<InfoClient> InfoClientTask(string nif)
    {
        var client = await _context.Clients.SingleAsync(x => x.Nif == nif);

        var infoClient = new InfoClient
        {
            Nif = client.Nif,
            NameResponsible = client.NameResponsible,
            ContactResponsible = client.ContactResponsible,
            EmailResponsible = client.EmailResponsible,
            Address1 = client.Address1,
            Address2 = client.Address2,
            City = client.City,
            Name = client.Name,
            PostalCode = client.PostalCode,
            BuyList = listBuyClient(""),
            ProjectsList = listProjectsClient("")
        };

        return infoClient;
    }

    public async Task CreateClientTask(CreateClient client)
    {
        var newClient = new Client
        {
            Address1 = client.Address1,
            Address2 = client.Address2,
            City = client.City,
            ContactResponsible = client.ContactResponsible,
            EmailResponsible = client.EmailResponsible,
            Name = client.Name,
            NameResponsible = client.NameResponsible,
            Nif = client.Nif,
            PostalCode = client.PostalCode,
            Active = true
        };

        await _context.Clients.AddAsync(newClient);
        await _context.SaveChangesAsync();
    }

    public async Task EditClientTask(CreateClient client)
    {
        var result = await _context.Clients.SingleAsync(x => x.Nif == client.Nif);
        result.Address1 = client.Address1;
        result.Address2 = client.Address2;
        result.City = client.City;
        result.ContactResponsible = client.ContactResponsible;
        result.EmailResponsible = client.EmailResponsible;
        result.Name = client.Name;
        result.NameResponsible = client.NameResponsible;
        result.PostalCode = client.PostalCode;

        await _context.SaveChangesAsync();
    }

    public async Task DisableClientTask(string nif)
    {
        var result = await _context.Clients.SingleAsync(x => x.Nif == nif);

        result.Active = false;

        await _context.SaveChangesAsync();
    }

    private List<ListBuyClient> listBuyClient(string nif)
    {
        return null;
    }

    private List<ListProjectsClient> listProjectsClient(string nif)
    {
        return null;
    }
}