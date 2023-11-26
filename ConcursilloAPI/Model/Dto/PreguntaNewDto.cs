using ConcursilloAPI.Model.Enums;

namespace ConcursilloAPI.Model.Dto
{
    public class PreguntaNewDto
    {
        public required string Texto { get; set; }
        public int Dificultad { get; set; }
        public List<RespuestaNewDto> Respuestas { get; set; } = new();
    }
}
