using EM.DataRepository.Products;

namespace EM.DataRepository.Stock;

public class Exit
{
    public DateTime Sell { get; set; }
    public string Reference { get; set; }
    public string Client { get; set; }
    public int Warehouse { get; set; }
    public float Total { get; set; }
    public List<AddProduct> Products { get; set; }
}