namespace EM.DatabaseSQL.Tables
{
    public partial class StatesBudget
    {
        public StatesBudget()
        {
            Budgets = new HashSet<Budget>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
