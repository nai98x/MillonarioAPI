using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using ConcursilloAPI.Model.Database;
using ConcursilloAPI.Model.Dto;

namespace ConcursilloAPI.Helpers
{
    public class CommonHelper
    {
        private Pregunta? _preguntaActual;
        private PreguntaDto? _preguntaActualDto;
        private int? _rondaActual;

        public FirestoreDb GetFirestoreClientAsync()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"firebase.json";
            var jsonString = File.ReadAllText(path);
            var builder = new FirestoreClientBuilder { JsonCredentials = jsonString };
            return FirestoreDb.Create("anilistconenie-e09cb", builder.Build());
        }

        public void IniciarJuego()
        {
            _rondaActual = 1;
        }

        public void FinalizarJuego()
        {
            _preguntaActual = null;
            _preguntaActualDto = null;
            _rondaActual = null;
        }

        public void SetPregunta(Pregunta pregunta)
        {
            _preguntaActual = pregunta;
        }

        public void SetPreguntaDto(PreguntaDto pregunta)
        {
            _preguntaActualDto = pregunta;
        }

        public Pregunta? GetPregunta()
        {
            return _preguntaActual;
        }

        public PreguntaDto? GetPreguntaDto()
        {
            return _preguntaActualDto;
        }


        public int? GetRonda()
        {
            return _rondaActual;
        }

        public void AvanzarRonda()
        {
            _rondaActual++;
        }

        public char GetLetraRespuesta(Respuesta respuesta, PreguntaDto preguntaDto)
        {
            if (preguntaDto.RespuestaA.Respuesta == respuesta.Texto)
            {
                return 'A';
            }
            else if (preguntaDto.RespuestaB.Respuesta == respuesta.Texto)
            {
                return 'B';
            }
            else if (preguntaDto.RespuestaC.Respuesta == respuesta.Texto)
            {
                return 'C';
            }
            else
            {
                return 'D';
            }
        }
    }
}
