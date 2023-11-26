using Google.Cloud.Firestore;

namespace ConcursilloAPI.Model.Database
{
    [FirestoreData]
    public class Pregunta
    {
        [FirestoreProperty]
        public string Texto { get; set; }

        public List<Respuesta> Respuestas { get; set; } = new();
    }
}
