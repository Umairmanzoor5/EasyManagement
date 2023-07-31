using System.ComponentModel.DataAnnotations;

namespace EM.DataRepository.Clients;

public class CreateClient
{
    [Required]
    public string Nif { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? NameResponsible { get; set; }
    public string? ContactResponsible { get; set; }
    public string? EmailResponsible { get; set; }
}