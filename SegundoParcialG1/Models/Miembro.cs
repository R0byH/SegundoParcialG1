namespace SegundoParcialG1.Models
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;

        public RolMiembro Rol { get; set; }

        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }

}
