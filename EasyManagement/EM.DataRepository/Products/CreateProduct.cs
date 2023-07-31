using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Products
{
	public class CreateProduct
	{
		[Required]
		public string Reference { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int IdFamily { get; set; }
		public int IdUnit { get; set; }
		public int? Barcode { get; set; }
		public int IdTypeProduct { get; set; }
		public bool StockControl { get; set; }
		public double? StockAlert { get; set; }
		public int IdTax { get; set; }
		public decimal Sale { get; set; }
		public decimal Purchase { get; set; }
		public string IdSuppliers { get; set; } = null!;
	}
}
