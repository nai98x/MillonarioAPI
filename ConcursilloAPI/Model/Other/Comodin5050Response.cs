namespace ConcursilloAPI.Model.Other
{
    public class Comodin5050Response
    {
        public char Descartado1 { get; set; }
        public char Descartado2 { get; set; }

        public Comodin5050Response(char descartado1, char descartado2)
        {
            Descartado1 = descartado1;
            Descartado2 = descartado2;
        }
    }
}
