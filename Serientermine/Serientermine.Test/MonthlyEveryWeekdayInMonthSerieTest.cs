using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serientermine.Series;

namespace Serientermine.Test
{
    internal class MonthlyEveryWeekdayInMonthSerieTest
    {
        private MonthlySerie? _serie;

        [SetUp]
        public void Setup()
        {
            _serie = new MonthlySerie
            {
                Begin = new DateTime(2021, 1, 1),
                End = new DateTime(2021, 7, 30),
                Intervall = 3,
                //DayList = new List<string> { "Wednesday" },
                MonthDay = 3
            };
        }

        [Test]
        public void TestWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2021, 1, 2), new DateTime(2021, 4, 30)).ToArray();
            var expected = new List<DateTime> { new(2021, 1, 20), new(2021, 4, 21) };
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
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 01), new DateTime(2021, 4, 17)).ToArray();
            var expected = new List<DateTime> { new(2021, 1, 20) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestEndWithinRange()
        {
            var actual = _serie!.GetDatesInRange(new DateTime(2020, 12, 20), new DateTime(2021, 8, 30)).ToArray();
            var expected = new List<DateTime> { new(2021, 1, 20), new(2021, 04, 21), new(2021, 7, 21) };
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
