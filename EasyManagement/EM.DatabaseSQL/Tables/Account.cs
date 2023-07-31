using System;
using System.Collections.Generic;

namespace EM.DatabaseSQL.Tables
{
    public partial class Account
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal? Salary { get; set; }
    }
}
