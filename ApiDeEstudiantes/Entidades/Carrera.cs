namespace ApiDeEstudiantes.Entidades
{
    public class Carrera
    {
        public int CarreraId { get; set; }
        public string NombreCarrera { get; set; }
        public ICollection<Estudiante> Estudiantes { get; set; }
    }
}
