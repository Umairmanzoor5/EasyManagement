using EM.DataRepository.Products;

namespace EM.DataRepository.Stock;

public class Entry
{
    public DateTime Buy { get; set; }
    public string Reference { get; set; }
    public string Supplier { get; set; }
    public int Warehouse { get; set; }
    public float Total { get; set; }
    public List<AddProduct> Products { get; set; }
}