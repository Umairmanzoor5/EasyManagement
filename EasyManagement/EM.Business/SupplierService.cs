using EM.DatabaseSQL.Contexts;
using EM.DatabaseSQL.Tables;
using EM.DataRepository.Clients;
using EM.DataRepository.Suppliers;
using EM.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EM.Business;

public class SupplierService : ISupplierService
{
	private readonly ApplicationDbContext _context;

	public SupplierService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<List<ListSuppliers>> ListSupplierTask()
	{
		var listSuppiler = await _context.Suppliers.Where(x => x.Active == true).Select(supplier => new ListSuppliers()
		{
			Name = supplier.Name,
			Nif = supplier.Nif,
			NameComercial = supplier.NameComercial,
			ContactComercial = supplier.ContactComercial,
			EmailComercial = supplier.EmailComercial
		})
		.ToListAsync();

		return listSuppiler;
	}

	public async Task<InfoSupplier> InfoSupplierTask(string nif)
	{
		var supplier = await _context.Suppliers.SingleAsync(x => x.Nif == nif);

		var infoSupplier = new InfoSupplier()
		{
			Name = supplier.Name,
			Nif = supplier.Nif,
			Address1 = supplier.Address1,
			Address2 = supplier.Address2,
			PostalCode = supplier.PostalCode,
			City = supplier.City,
			NameComercial = supplier.NameComercial,
			ContactComercial = supplier.ContactComercial,
			EmailComercial = supplier.EmailComercial,
			BuyProductList = listBuyProductSuplier(""),
			InvoicingList = listInvoicingSupplier("")
	};

		return infoSupplier;
	}

	public async Task CreateSupplierTask(CreateSupplier supplier)
	{
		var newSupplier = new Supplier
		{
			Name = supplier.Name,
			Nif = supplier.Nif,
			Address1 = supplier.Address1,
			Address2 = supplier.Address2,
			PostalCode = supplier.PostalCode,
			City = supplier.City,
			NameComercial = supplier.NameComercial,
			ContactComercial = supplier.ContactComercial,
			EmailComercial = supplier.EmailComercial,
			Active = true
		};

		await _context.Suppliers.AddAsync(newSupplier);
		await _context.SaveChangesAsync();
	}

	public async Task EditSupplierTask(CreateSupplier supplier)
	{
		var result = await _context.Suppliers.SingleAsync(x => x.Nif == supplier.Nif);
		result.Name = supplier.Name;
		result.Nif = supplier.Nif;
		result.Address1 = supplier.Address1;
		result.Address2 = supplier.Address2;
		result.PostalCode = supplier.PostalCode;
		result.City = supplier.City;
		result.NameComercial = supplier.NameComercial;
		result.ContactComercial = supplier.ContactComercial;
		result.EmailComercial = supplier.EmailComercial;

		await _context.SaveChangesAsync();
	}


	public async Task DisableSupplierTask(string nif)
	{
		var result = await _context.Suppliers.SingleAsync(x => x.Nif == nif);

		result.Active = false;
		await _context.SaveChangesAsync();
	}

	private List<ListInvoicingSupplier> listInvoicingSupplier(string nif)
	{
		return null;
	}

	private List<ListBuyProductSupplier> listBuyProductSuplier(string nif)
	{
		return null;
	}
}

