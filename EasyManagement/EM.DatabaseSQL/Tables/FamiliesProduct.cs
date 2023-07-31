using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class FamiliesProduct
    {
        public FamiliesProduct()
        {
            Manufacturings = new HashSet<Manufacturing>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Manufacturing> Manufacturings { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
