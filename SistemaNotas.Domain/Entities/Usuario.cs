namespace SistemaNotas.Domain.Entities;

public class Usuario : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Presentacion> Presentaciones { get; set; } = new List<Presentacion>();
}