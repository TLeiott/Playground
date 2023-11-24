using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serientermine.Providers
{
    internal interface IWriter
    {
        void Write(ISerie serie, DateTime start, DateTime end, DateTime rangeStart, DateTime rangeEnd);
    }
}
