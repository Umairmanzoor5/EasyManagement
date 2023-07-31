using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.DataRepository.Stock
{
	public class Transfer
	{
		public string? Reference { get; set; }
		public int WarehouseExit { get; set; }
		public int WarehouseEntry { get; set; }
		public List<TransferProducts> Products { get; set; }
	}

	public class TransferProducts
	{
		public string Reference { get; set; }
		public double Quantity { get; set; }
	}
}
