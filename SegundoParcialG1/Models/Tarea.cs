namespace SegundoParcialG1.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public EstadoTarea Estado { get; set; } = EstadoTarea.ToDo;

        // Relación con Miembro
        public int IdResponsable { get; set; }
        public Miembro? Responsable { get; set; }

        // Relación con Prioridad
        public int PrioridadId { get; set; }
        public Prioridad? Prioridad { get; set; }
    }

}
