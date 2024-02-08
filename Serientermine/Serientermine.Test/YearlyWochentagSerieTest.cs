using Serientermine.Series;

namespace Serientermine.Test
{
    internal class YearlyWochentagSerieTest
    {
        private YearlySerie? _serie;

        [SetUp]
        public void Setup()
        {
            _serie = new YearlySerie
            {
                Begin = new DateTime(2021, 1, 1),
                End = new DateTime(2030, 7, 30),
                Intervall = 3,
                //DayList = new List<string> { "Monday" },
                MonthDay = 2,
                Month=4
            };
        }

        [Test]
        public void TestWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 1, 2), new DateTime(2029, 4, 30)).ToList();
            var expected = new List<DateTime> { new(2021, 4, 12), new(2024, 4, 8), new(2027, 4, 12) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestOuterRangeBefore()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 1, 1), new DateTime(2020, 12, 10)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestOuterRangeAfter()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2022, 1, 1), new DateTime(2022, 12, 10)).ToArray();
            Assert.That(actual, Is.EquivalentTo(Array.Empty<DateTime>()));
        }

        [Test]
        public void TestBeginWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 01), new DateTime(2026, 4, 17)).ToList();
            var expected = new List<DateTime> { new(2021, 4, 12), new(2024, 4, 8) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestEndWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 20), new DateTime(2031, 8, 30)).ToList();
            var expected = new List<DateTime> { new(2021, 4, 12), new(2024, 4, 8), new(2027, 4, 12), new(2030, 4, 8) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
