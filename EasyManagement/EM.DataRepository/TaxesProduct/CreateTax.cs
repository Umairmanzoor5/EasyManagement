namespace EM.DataRepository.TaxesProduct;

public class CreateTax
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Tax { get; set; }
}