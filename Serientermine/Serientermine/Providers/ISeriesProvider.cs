using Serientermine.Series;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Serientermine.Providers
{
    internal interface ISeriesProvider
    {
        Task CheckDbStructureAsync(CancellationToken token);

        Task<List<SerieBase>> GetSeriesAsync(CancellationToken token);

        Task CreateAsync(ISerie serie, CancellationToken token);
        Task UpdateAsync(ISerie serie, CancellationToken token);
    }
}
