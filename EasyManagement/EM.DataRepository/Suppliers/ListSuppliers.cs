using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Suppliers;

public class ListSuppliers
{
	public string Nif { get; set; }
	public string Name { get; set; }
	public string? NameComercial { get; set; }
	public string? ContactComercial { get; set; }
	public string? EmailComercial { get; set; }
}