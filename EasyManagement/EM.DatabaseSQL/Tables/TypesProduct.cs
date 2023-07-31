using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class TypesProduct
    {
        public TypesProduct()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
