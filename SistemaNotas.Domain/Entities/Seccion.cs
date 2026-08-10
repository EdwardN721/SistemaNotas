namespace SistemaNotas.Domain.Entities
{
    public class Seccion : EntityBase
    {
        public Guid PresentacionId { get; set; }
        public int Orden { get; set; }
        public string TituloSeccion { get; set; } = string.Empty;
        public int? MinutosEstimados { get; set; }

        // Navegacion
        public virtual Presentacion? Presentacion { get; set; }
        public virtual ICollection<Ancla> Anclas { get; set; } = new List<Ancla>();
    }
}