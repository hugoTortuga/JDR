using JDR.Modele;

namespace JDR.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DoesIsItCrashWhenIRoll()
        {
            new Dice(20).Roll();
            Assert.Pass();
        }

        [Test]
        public void NegativeNumberOfFace()
        {
            Assert.Throws(typeof(ArgumentException), () => new Dice(-1));
        }

        [Test]
        public void NullNumberOfFace()
        {
            Assert.Throws(typeof(ArgumentException), () => new Dice(0));
        }
    }
}