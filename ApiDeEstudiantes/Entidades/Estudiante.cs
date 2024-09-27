namespace ApiDeEstudiantes.Entidades
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string MatriculaId { get; set; }
        public string NombreEstudiante { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
    }
}
