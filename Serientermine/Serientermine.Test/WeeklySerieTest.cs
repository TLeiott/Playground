using Serientermine.Series;

namespace Serientermine.Test
{
    internal class WeeklySerieTest
    {
        private WeeklySerie? _serie;

        [SetUp]
        public void Setup()
        {
            _serie = new WeeklySerie
            {
                Begin = new DateTime(2021, 1, 1),
                End = new DateTime(2021, 3, 31),
                Intervall = 3,
                DayList = new List<string> { "Thursday", "Tuesday", "Thursday" }
            };
        }

        [Test]
        public void TestWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 1, 6), new DateTime(2021, 2, 10)).ToList();
            var expected = new List<DateTime> { new(2021, 1, 19), new(2021, 1, 21), new(2021, 2, 9) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestOuterRangeBefore()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 1, 6), new DateTime(2020, 2, 10)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestOuterRangeAfter()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2022, 1, 6), new DateTime(2022, 2, 10)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestBeginWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 3, 16), new DateTime(2021, 5, 10)).ToList();
            var expected = new List<DateTime> { new(2021, 3, 23), new(2021, 3, 25) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestEndWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 20), new DateTime(2021, 2, 10)).ToList();
            var expected = new List<DateTime> { new(2021, 1, 19), new(2021, 1, 21), new(2021, 2, 9) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
