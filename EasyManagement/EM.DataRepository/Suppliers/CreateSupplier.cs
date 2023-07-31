using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Suppliers;

public class CreateSupplier
{
	[Required]
	public string Nif { get; set; } = null!;
	[Required]
	public string Name { get; set; } = null!;
	public string? Address1 { get; set; }
	public string? Address2 { get; set; }
	public string? PostalCode { get; set; }
	public string? City { get; set; }
	public string? NameComercial { get; set; }
	public string? ContactComercial { get; set; }
	public string? EmailComercial { get; set; }
}