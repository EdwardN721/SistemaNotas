using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Domain.Entities
{
    public class Presentacion : EntityBase
    {
        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        public string Titulo { get; set; } = string.Empty;
        public DateTimeOffset? FechaExposicion { get; set; }
        public string? Audiencia { get; set; }

        // Navegacion
        public virtual ICollection<Seccion> Secciones { get; set; } = new List<Seccion>();  
        public virtual Retrospectiva? Retrospectiva { get; set; }
    }
}