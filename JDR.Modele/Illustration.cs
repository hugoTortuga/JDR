namespace JDR.Model
{
    public class Illustration
    {
        public Guid Id { get; set; }
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