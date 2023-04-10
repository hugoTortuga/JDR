namespace JDR.Model
{
    public class Illustration
    {

        public string? Name { get; set; }
        public string? Extension { get; set; }
        public byte[]? Content { get; set; }

        public Illustration()
        {
        }

		public static Illustration None() {
            return new Illustration();
        }

    }
}