using System.ComponentModel.DataAnnotations;

namespace EM.DataRepository.Types;

public class CreateType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}