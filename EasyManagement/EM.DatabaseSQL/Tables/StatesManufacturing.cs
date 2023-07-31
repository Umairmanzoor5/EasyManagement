using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class StatesManufacturing
    {
        public StatesManufacturing()
        {
            Manufacturings = new HashSet<Manufacturing>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Manufacturing> Manufacturings { get; set; }
    }
}
