using ApiDeEstudiantes.Models.Carreras;

namespace ApiDeEstudiantes.Models.Estudiantes
{
    public class EstudianteGetModel
    {
        public int EstudianteId { get; set; }
        public string MatriculaId { get; set; }
        public string NombreEstudiante { get; set; }
        public int CarreraId { get; set; }
        public CarreraModel Carrera { get; set; }
    }
}
