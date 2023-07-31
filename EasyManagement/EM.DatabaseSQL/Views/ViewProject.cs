using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Views
{
    public partial class ViewProject
    {
        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Client { get; set; } = null!;
        public decimal? Value { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
