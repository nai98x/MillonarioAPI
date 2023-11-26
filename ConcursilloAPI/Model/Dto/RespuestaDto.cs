namespace ConcursilloAPI.Model.Dto
{
    public class RespuestaDto
    {
        public string Respuesta { get; set; }

        public RespuestaDto(string respuesta)
        {
            Respuesta = respuesta;
        }
    }
}
