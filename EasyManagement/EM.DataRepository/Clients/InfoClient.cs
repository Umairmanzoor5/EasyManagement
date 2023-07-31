namespace EM.DataRepository.Clients;

public class InfoClient : CreateClient
{
    public List<ListProjectsClient> ProjectsList { get; set; }
    public List<ListBuyClient> BuyList { get; set; }
}