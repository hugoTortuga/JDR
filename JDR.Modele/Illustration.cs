namespace JDR.Model
{
    public class Illustration
    {

        public Uri Uri { get; set; }
        public Illustration(string URI)
        {
            Uri = new Uri("C:\\Users\\Hugo\\Desktop\\jdr\\ArthosV2\\" + URI);
        }


		public static Illustration None() {
            return new Illustration("");
        }

    }
}