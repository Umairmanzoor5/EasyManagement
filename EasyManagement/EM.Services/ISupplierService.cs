using EM.DataRepository.Clients;
using EM.DataRepository.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Services;

public interface ISupplierService
{
	Task<List<ListSuppliers>> ListSupplierTask();
	Task<InfoSupplier> InfoSupplierTask(string nif);
	Task CreateSupplierTask(CreateSupplier supplier);
	Task EditSupplierTask(CreateSupplier supplier);
	Task DisableSupplierTask(string nif);
}