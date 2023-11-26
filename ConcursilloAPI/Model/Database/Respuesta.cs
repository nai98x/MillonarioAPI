using Google.Cloud.Firestore;

namespace ConcursilloAPI.Model.Database
{
    [FirestoreData]
    public class Respuesta
    {
        [FirestoreProperty]
        public string Texto { get; set; }

        [FirestoreProperty]
        public bool Correcta { get; set; }
    }
}
