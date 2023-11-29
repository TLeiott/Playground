
using Serientermine.Series;

namespace Serientermine.Test
{
    public class DailySerieTest
    {
        private DailySerie? _serie;

        [SetUp]
        public void Setup()
        {
            _serie = new DailySerie
            {
                Begin = new DateTime(2021, 1, 1),
                End = new DateTime(2021, 1, 31),
                Intervall = 3
            };
        }

        [Test]
        public void TestWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 1, 5), new DateTime(2021, 1, 11)).ToList();
            var expected = new List<DateTime> { new(2021, 1, 7), new(2021, 1, 10) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestOuterRangeBefore()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 1, 5), new DateTime(2020, 1, 11)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestOuterRangeAfter()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2022, 1, 5), new DateTime(2022, 1, 11)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestBeginWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 1, 26), new DateTime(2021, 2, 5)).ToList();
            var expected = new List<DateTime> { new(2021, 1, 28), new(2021, 1, 31) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestEndWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 27), new DateTime(2021, 1, 6)).ToList();
            var expected = new List<DateTime> { new(2021, 1, 1), new(2021, 1, 4) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}