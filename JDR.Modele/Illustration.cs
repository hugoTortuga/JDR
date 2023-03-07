namespace JDR.Model
{
    public class Illustration
    {

        public string URL { get; set; }
        public Illustration(string uRL)
        {
            URL = uRL;
        }

        public static Illustration None() {
            return new Illustration("/noIllustration.jpg");
        }

    }
}