using Serientermine.Series;

namespace Serientermine.Test
{
    internal class MonthlySerieTest
    {
        private MonthlySerie? _serie;

        [SetUp]
        public void Setup()
        {
            _serie = new MonthlySerie
            {
                Begin = new DateTime(2023, 1, 1),
                End = new DateTime(2023, 4, 30),
                Intervall = 2,
                DayList = new List<string> { "Monday" },
                MonthDay = 5
            };
        }

        [Test]
        public void TestWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2022, 12, 31), new DateTime(2023, 12, 31)).ToList();
            var expected = new List<DateTime> { new(2023, 1, 30), new(2023, 3, 27) };
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
            var actual = _serie!.GetDatesInRange(new DateTime(2026, 1, 6), new DateTime(2026, 2, 10)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestBeginWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2022, 12, 1), new DateTime(2023, 2, 1)).ToList();
            var expected = new List<DateTime> { new(2023, 1, 30) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestEndWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2023, 2, 1), new DateTime(2023, 5, 10)).ToList();
            var expected = new List<DateTime> { new(2023, 3, 27) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
