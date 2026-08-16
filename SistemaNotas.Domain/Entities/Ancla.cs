namespace SistemaNotas.Domain.Entities
{
    public class Ancla : EntityBase
    {
        public Guid SeccionId { get; set; }
        public Guid CategoriaId { get; set; }
        public int Orden { get; set; }
        public string ConceptoClave { get; set; } = string.Empty;
        public bool RecordatorioVisual { get; set; } 

        // Navegacion
        public virtual Seccion? Seccion { get; set; } = null!;
        public virtual CategoriaAncla? Categoria { get; set; } = null!; 
    }
}