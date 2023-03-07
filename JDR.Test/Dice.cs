using JDR.Model;

namespace JDR.Test
{
    public class DiceTest
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

		[Test]
        [Repeat(100)]
		public void RollDice20between1and20() {
			var result = new Dice(20).Roll();
            Assert.That(result, Is.GreaterThanOrEqualTo(1));
			Assert.That(result, Is.AtMost(20));
		}

		[Test]
        [TestCase(10)]
		[TestCase(20)]
		[TestCase(50)]
		[TestCase(100)]
		public void RollDiceBetween1andNumberOfFace(int numberOfFace) {
			var result = new Dice(numberOfFace).Roll();
			Assert.That(result, Is.GreaterThanOrEqualTo(1));
			Assert.That(result, Is.AtMost(numberOfFace));
		}

	}
}