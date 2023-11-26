using ConcursilloAPI.Model.Enums;

namespace ConcursilloAPI.Model.Dto
{
    public class PreguntaDto
    {
        public string Pregunta { get; set; }
        public RespuestaDto RespuestaA { get; set; }
        public RespuestaDto RespuestaB { get; set; }
        public RespuestaDto RespuestaC { get; set; }
        public RespuestaDto RespuestaD { get; set; }

        public PreguntaDto(string pregunta, string respuestaA, string respuestaB, string respuestaC, string respuestaD)
        {
            Pregunta = pregunta;
            RespuestaA = new RespuestaDto(respuestaA);
            RespuestaB = new RespuestaDto(respuestaB);
            RespuestaC = new RespuestaDto(respuestaC);
            RespuestaD = new RespuestaDto(respuestaD);
        }
    }
}
