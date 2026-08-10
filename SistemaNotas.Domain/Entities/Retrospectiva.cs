namespace SistemaNotas.Domain.Entities
{
    public class Retrospectiva
    {
        public Guid PresentacionId { get; set; }
        public int NivelNerviosismo { get; set; }
        public List<string>? MuletillasDetectadas { get; set; } = new List<string>();
        public string? QueSalioBien { get; set; }

        // Navegacion
        public virtual Presentacion? Presentacion { get; set; }
    }
}