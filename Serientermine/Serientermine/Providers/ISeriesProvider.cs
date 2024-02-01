using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Providers
{
    internal interface ISeriesProvider
    {
        List<ISerie> GetSeries();
    }
}
