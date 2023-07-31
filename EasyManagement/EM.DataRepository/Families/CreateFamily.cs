using System.ComponentModel.DataAnnotations;

namespace EM.DataRepository.Families;

public class CreateFamily
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}