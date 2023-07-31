using EM.DataRepository.TaxesProduct;

namespace EM.Services;

public interface ITaxProductService
{
    Task<List<ListTaxesProducts>> ListTaxTask();
    Task<InfoTax> InfoTaxTask(int id);
    Task CreateTaxTask(CreateTax tax);
    Task EditTaxTask(CreateTax tax);
    Task DisableTaxTask(int id);
}