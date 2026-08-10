using SistemaNotas.Domain.Entities;

namespace SistemaNotas.Domain.Entities
{
    public class Presentacion : EntityBase
    {
        public string Titulo { get; set; } = string.Empty;
        public DateTimeOffset FechaExposicion { get; set; }
        public string? Audiciencia { get; set; }

        // Navegacion
        public virtual ICollection<Seccion> Secciones { get; set; } = new List<Seccion>();  
        public virtual Retrospectiva? Retrospectiva { get; set; }
    }
}