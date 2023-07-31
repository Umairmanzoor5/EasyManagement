using EM.DataRepository.Products;
using System.ComponentModel.DataAnnotations;

namespace EM.DataRepository.Budgets;

public class CreateBudget
{
    public string? Description { get; set; }
    public string? IdProject { get; set; }
    public string Client { get; set; }
    public List<AddProduct>? Products { get; set; }
    public float? Total { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }    

}

