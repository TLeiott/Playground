using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;
using Serientermine.Serientermine;
using Serientermine.Series;

namespace SerientermineErmitteln.Data.Database
{
    public class DatabaseProvider
    {
        private const string CreateTableSql = @"
            CREATE TABLE IF NOT EXISTS Series (
                Id INTEGER PRIMARY KEY,
                SerieType INTEGER NOT NULL,
                Name TEXT,
                Begin TEXT NOT NULL,
                End TEXT,
                Intervall INTEGER NOT NULL,
                Limit INTEGER,
                WeekDay TEXT,
                MonthDay INTEGER,
                Month INTEGER
            );";

        private async Task<SqliteConnection> GetConnectionAndCheckDbStructureAsync(CancellationToken token)
        {
            var connectionString = "Data Source=serientermine.db";
            var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync(token);

            using var command = connection.CreateCommand();
            command.CommandText = CreateTableSql;
            await command.ExecuteNonQueryAsync(token);

            return connection;
        }




        public async Task<List<ISerie>> GetSeries(CancellationToken token)
        {
            using var connection = await GetConnectionAndCheckDbStructureAsync(token);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Series";

            using var reader = await command.ExecuteReaderAsync(token);
            var series = new List<ISerie>();

            while (await reader.ReadAsync(token))
            {
                var seriesType = await reader.GetFieldValueAsync<string>(1, token);
                SerieBase serie;

                switch (seriesType)
                {
                    case "Daily":
                        serie = new DailySerie();
                        break;
                    case "Weekly":
                        serie = new WeeklySerie();
                        break;
                    case "Monthly":
                        serie = new MonthlySerie();
                        break;
                    case "Yearly":
                        serie = new YearlySerie();
                        break;
                    default:
                        throw new NotSupportedException($"Der Serientyp '{seriesType}' ist noch nicht implementiert.");
                }

                series.Add(serie);
            }

            return series;
        }
    }
}