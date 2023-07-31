namespace EM.DatabaseSQL.Views
{
    public partial class ViewCurrentStock
    {
        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string NameFamily { get; set; } = null!;
        public double Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Pl { get; set; }
        public decimal Pvp { get; set; }
    }
}
