using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class UnitiesProduct
    {
        public UnitiesProduct()
        {
            Manufacturings = new HashSet<Manufacturing>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Manufacturing> Manufacturings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
