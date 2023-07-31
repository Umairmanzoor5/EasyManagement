namespace EM.DataRepository.Stock;

public class AdjustStock
{
    public string? Reference { get; set; }
    public int Warehouse { get; set; }
    public List<AdjustProducts> Products { get; set; }
}

public class AdjustProducts
{
    public string Reference { get; set; }
    public double OldQuantity { get; set; }
    public double NewQuantity { get; set; }
}