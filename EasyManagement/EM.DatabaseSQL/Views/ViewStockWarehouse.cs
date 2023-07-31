using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Views
{
    public partial class ViewStockWarehouse
    {
        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? Warehouse { get; set; }
        public double StockNow { get; set; }
    }
}
