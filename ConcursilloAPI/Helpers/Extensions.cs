using ConcursilloAPI.Model.Database;
using ConcursilloAPI.Model.Dto;
using ConcursilloAPI.Model.Enums;

namespace ConcursilloAPI.Helpers
{
    public static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }

        public static PreguntaDto ToPreguntaDto(this Pregunta pregunta)
        {
            var respuestasMezcladas = pregunta.Respuestas.OrderBy(x => Guid.NewGuid()).ToList();

            return new PreguntaDto(pregunta.Texto, respuestasMezcladas[0].Texto, respuestasMezcladas[1].Texto, respuestasMezcladas[2].Texto, respuestasMezcladas[3].Texto);
        }
    }
}
