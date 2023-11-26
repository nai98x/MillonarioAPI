namespace ConcursilloAPI.Model.Other
{
    public class JuegoFinalizadoResponse
    {
        public string Message { get; set; }
        public char RespuestaCorrecta { get; set; }

        public JuegoFinalizadoResponse(string message, char respuestaCorrecta)
        {
            Message = message;
            RespuestaCorrecta = respuestaCorrecta;
        }
    }
}
