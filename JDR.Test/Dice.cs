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

        [Test]
        public void CreateDices() {
            var dices = new Dices("1d6");
            Assert.Pass();
        }

		[Test]
		public void CreateDices1d6CheckNumber() {
			var dices = new Dices("1d6");
			Assert.That(dices.PlayDices?.Count, Is.EqualTo(1));
		}

		[Test]
		public void CreateDices1d6CheckFaces() {
			var dices = new Dices("1d6");
			Assert.That(dices.PlayDices[0].NumberFaces, Is.EqualTo(6));
		}

		[Test]
		[TestCase(1, 6)]
		[TestCase(4, 6)]
		[TestCase(1, 100)]
		[TestCase(4, 20)]
		public void CreateDicesParameters(int numberOfDice, int numberOfFace) {
			var dices = new Dices($"{numberOfDice}d{numberOfFace}");
			Assert.That(dices.PlayDices?.Count, Is.EqualTo(numberOfDice));
			Assert.That(dices.PlayDices[0].NumberFaces, Is.EqualTo(numberOfFace));
		}

	}
}