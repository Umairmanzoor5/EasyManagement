using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class ProductsManufacturing
    {
        public string IdManufacturing { get; set; } = null!;
        public string IdProducts { get; set; } = null!;
        public string Quantity { get; set; } = null!;

        public virtual Manufacturing IdManufacturingNavigation { get; set; } = null!;
        public virtual Product IdProductsNavigation { get; set; } = null!;
    }
}
