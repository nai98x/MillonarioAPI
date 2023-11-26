using ConcursilloAPI.Helpers;
using ConcursilloAPI.Model.Database;
using ConcursilloAPI.Model.Dto;
using ConcursilloAPI.Model.Enums;
using Google.Cloud.Firestore;
using System.Collections.Generic;

namespace ConcursilloAPI.Repositories
{
    public class PreguntasRepository
    {
        private readonly CommonHelper _commonHelper;
        private FirestoreDb _db;

        public PreguntasRepository(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _db = _commonHelper.GetFirestoreClientAsync();
        }

        public async Task SetPregunta(PreguntaNewDto pregunta)
        {
            CollectionReference col = _db.Collection("Concursillo").Document($"{(int)pregunta.Dificultad}").Collection("Preguntas");

            Dictionary<string, object> data = new()
            {
                { "Texto", pregunta.Texto }
            };

            DocumentReference doc = await col.AddAsync(data);

            CollectionReference colRespuestas = doc.Collection("Respuestas");
            foreach (var respuesta in pregunta.Respuestas)
            {
                Dictionary<string, object> dataRespuesta = new()
                {
                    { "Texto", respuesta.Texto },
                    { "Correcta", respuesta.Correcta }
                };

                await colRespuestas.AddAsync(dataRespuesta);
            }
        }

        public async Task<Pregunta> GetPregunta(Dificultad dificultad)
        {
            var ret = new List<Pregunta>();

            _db = _commonHelper.GetFirestoreClientAsync();
            CollectionReference col = _db.Collection("Concursillo").Document($"{(int)dificultad}").Collection("Preguntas");
            var snap = await col.GetSnapshotAsync();

            foreach (var document in snap.Documents)
            {
                var pregunta = document.ConvertTo<Pregunta>();

                CollectionReference colRespuestas = document.Reference.Collection("Respuestas");
                var snapRespuestas = await colRespuestas.GetSnapshotAsync();

                foreach (var respuesta in snapRespuestas.Documents)
                {
                    pregunta.Respuestas.Add(respuesta.ConvertTo<Respuesta>());
                }

                ret.Add(pregunta);
            }

            return ret.OrderBy(x => Guid.NewGuid()).First();
        }
    }
}
