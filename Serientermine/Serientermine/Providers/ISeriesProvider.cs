using Serientermine.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Serientermine.Providers
{
    internal interface ISeriesProvider
    {
        Task<List<ISerie>> GetSeriesAsync(CancellationToken token);

        Task CreateAsync(ISerie serie, CancellationToken token);
        Task UpdateAsync(ISerie serie, CancellationToken token);
    }
}
