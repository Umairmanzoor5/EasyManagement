using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Suppliers;

public class InfoSupplier : CreateSupplier
{
	public List<ListInvoicingSupplier> InvoicingList { get; set; }
	public List<ListBuyProductSupplier> BuyProductList { get; set; }

}