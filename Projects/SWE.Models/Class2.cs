namespace SWE.Models
{
    public class Rectangle
    {
        public int Breite { get; set; }
        public int Länge { get; set; }
        public bool RechterWinkel { get; }
        public double Flaeche { get; set; }

        public class Circle
        {
            public int Durchmesser { get; set; }
            public int Radius { get; set; }
            public int Flaeche { get; set; }
            public int Umfang { get; set; }

        }
    }
}