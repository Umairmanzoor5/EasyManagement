using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Manufacturing
    {
        public Manufacturing()
        {
            ProductsManufacturings = new HashSet<ProductsManufacturing>();
        }

        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int IdFamily { get; set; }
        public int IdUnit { get; set; }
        public int IdState { get; set; }
        public int IdBudget { get; set; }

        public virtual Budget IdBudgetNavigation { get; set; } = null!;
        public virtual FamiliesProduct IdFamilyNavigation { get; set; } = null!;
        public virtual StatesManufacturing IdStateNavigation { get; set; } = null!;
        public virtual UnitiesProduct IdUnitNavigation { get; set; } = null!;
        public virtual ICollection<ProductsManufacturing> ProductsManufacturings { get; set; }
    }
}
