namespace JDR.Model
{
    public class Illustration
    {

        public Uri Uri { get; set; }
        public Illustration(string URI)
        {
            Uri = new Uri(URI);
        }


		public static Illustration None() {
            return new Illustration("none.jpg");
        }

    }
}