namespace JDR.Model
{
    public class Illustration
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] Content => File.ReadAllBytes($"{Name}.{Extension}");

        public Illustration(string name, string extension)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            ArgumentNullException.ThrowIfNullOrEmpty(extension);
            Name = name;
            Extension = extension;
        }

		public static Illustration None() {
            return new Illustration("nodata", "jpg");
        }

    }
}