using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Domain.Entities;

public class CategoriaAncla : EntityBase
{
    public string Nombre { get; set; } = string.Empty;
    public string? CodigoColor { get; set; } 
    public bool Activo { get; set; } = true;

    public virtual ICollection<Ancla> Anclas { get; set; } = new List<Ancla>(); 
}