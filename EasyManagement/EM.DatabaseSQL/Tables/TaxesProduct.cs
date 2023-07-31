using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class TaxesProduct
    {
        public TaxesProduct()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Tax { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
