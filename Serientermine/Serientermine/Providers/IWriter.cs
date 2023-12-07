using Serientermine.Series;
using System;

namespace Serientermine.Providers
{
    internal interface IWriter
    {
        void Write(ISerie serie, DateTime start, DateTime end, DateTime rangeStart, DateTime rangeEnd);
    }
}
