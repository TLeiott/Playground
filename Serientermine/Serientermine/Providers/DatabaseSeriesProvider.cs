﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serientermine.Series;
using SerientermineErmitteln.Data.Database;

namespace Serientermine.Providers
{
    internal class DatabaseSeriesProvider : ISeriesProvider
    {
        private bool _hasCheckDbStructure;
        private const string CreateTableSql = @"
            CREATE TABLE IF NOT EXISTS Series (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                SerieType INTEGER NOT NULL,
                Name TEXT,
                Begin TEXT NOT NULL,
                End TEXT,
                Intervall INTEGER NOT NULL,
                RepeatLimit INTEGER,
                WeekDay TEXT,
                MonthDay INTEGER,
                Month INTEGER
            );";
        private readonly TerminDbContext _context;

        public DatabaseSeriesProvider(TerminDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CheckDbStructureAsync(CancellationToken token)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync(token);
            await using var command = connection.CreateCommand();
            command.CommandText = CreateTableSql;
            await command.ExecuteNonQueryAsync(token);
            await connection.CloseAsync();
        }

        private async Task<SqliteConnection> GetConnectionAndCheckDbStructureAsync(CancellationToken token)
        {
            const string connectionString = "Data Source=serientermine.db";
            var connection = new SqliteConnection(connectionString);
            await connection.OpenAsync(token);
            if (!_hasCheckDbStructure)
            {
                await using var command = connection.CreateCommand();
                command.CommandText = CreateTableSql;
                await command.ExecuteNonQueryAsync(token);
                _hasCheckDbStructure = true;
            }
            return connection;
        }
        //Serie Laden
        public async Task<List<SerieBase>> GetSeriesAsync(CancellationToken token)
        {
            return await _context.TerminSerien
                //.Where(x => x.Begin <= DateTime.Today)
                .ToListAsync(token);
            

            //await using var connection = await GetConnectionAndCheckDbStructureAsync(token);
            //await using var command = connection.CreateCommand();
            //command.CommandText = "SELECT * FROM Series";
            //await using var reader = await command.ExecuteReaderAsync(token);
            //var series = new List<ISerie>();
            //while (await reader.ReadAsync(token))
            //{
            //    var seriesType = (SerieType)await reader.GetFieldValueAsync<int>(1, token);
            //    SerieBase serie;
            //    switch (seriesType)
            //    {
            //        case SerieType.Daily:
            //            serie = new DailySerie();
            //            break;
            //        case SerieType.Weekly:
            //            serie = new WeeklySerie();
            //            break;
            //        case SerieType.Monthly:
            //            serie = new MonthlySerie();
            //            break;
            //        case SerieType.Yearly:
            //            serie = new YearlySerie();
            //            break;
            //        default:
            //            throw new NotSupportedException($"Der Serientyp '{seriesType}' ist noch nicht implementiert.");
            //    }
            //    serie.Begin = DateTime.Parse(await reader.GetFieldValueAsync<string>(3, token));
            //    serie.Intervall = await reader.GetFieldValueAsync<int>(5, token);
            //    serie.Limit = await reader.GetFieldValueAsync<int>(6, token);
            //    serie.Month = await reader.GetFieldValueAsync<int>(9, token);
            //    serie.MonthDay = await reader.GetFieldValueAsync<int>(8, token);
            //    serie.WeekDay = await reader.GetFieldValueAsync<string>(7, token);
            //    serie.Name = await reader.GetFieldValueAsync<string>(2, token);
            //    serie.Id = await reader.GetFieldValueAsync<int>(0, token);
            //    string dateValue = await reader.GetFieldValueAsync<string>(4, token);
            //    DateTime? endDate = null;

            //    if (dateValue != null && DateTime.TryParse(dateValue, out DateTime parsedDate))
            //    {
            //        endDate = parsedDate;
            //    }
            //    else
            //    {
            //        // Behandeln Sie den Fall, in dem das Datum nicht geparst werden konnte oder null war
            //    }
            //    serie.End = endDate;
            //    series.Add(serie);
            //}
            //return series;
        }
        //Serie Speichern
        public async Task CreateAsync(ISerie serie, CancellationToken token)
        {
            using var connection = await GetConnectionAndCheckDbStructureAsync(token);
            using var command = connection.CreateCommand();
            //command.CommandText = "INSERT INTO Series (Name) VALUES ([Parameter])";
            command.CommandText = @"
            INSERT INTO Series (SerieType, Name, Begin, End, Intervall, RepeatLimit, WeekDay, MonthDay, Month)
            VALUES (@SerieType, @Name, @Begin, @End, @Intervall, @RepeatLimit, @WeekDay, @MonthDay, @Month);";
            command.Parameters.AddWithValue("@SerieType", (int)serie.Type1);
            command.Parameters.AddWithValue("@Intervall", serie.Intervall);
            command.Parameters.AddWithValue("@Name", serie.Name ?? "");
            command.Parameters.AddWithValue("@RepeatLimit", serie.Limit);
            command.Parameters.AddWithValue("@Begin", serie.Begin);
            command.Parameters.AddWithValue("@End", (object)serie.End ?? DBNull.Value);
            command.Parameters.AddWithValue("@WeekDay", serie.WeekDay ?? "");
            command.Parameters.AddWithValue("@MonthDay", serie.MonthDay);
            command.Parameters.AddWithValue("@Month", serie.Month);
            var result = await command.ExecuteNonQueryAsync(token);
        }
        //Serie Bearbeiten
        public async Task UpdateAsync(ISerie serie, CancellationToken token)
        {
            using var connection = await GetConnectionAndCheckDbStructureAsync(token);
            using var command = connection.CreateCommand();
            command.CommandText = @"UPDATE Series SET SerieType = @SerieType,Name = @Name,Begin = @Begin,End = @End,Intervall = @Intervall,RepeatLimit = @RepeatLimit,WeekDay = @WeekDay,MonthDay = @MonthDay, Month = @Month WHERE Id = @Id";
            command.Parameters.AddWithValue("@SerieType", (int)serie.Type1);
            command.Parameters.AddWithValue("@Id", serie.Id);
            command.Parameters.AddWithValue("@Intervall", serie.Intervall);
            command.Parameters.AddWithValue("@Name", serie.Name ?? "");
            command.Parameters.AddWithValue("@RepeatLimit", serie.Limit);
            command.Parameters.AddWithValue("@Begin", serie.Begin);
            command.Parameters.AddWithValue("@End", (object)serie.End ?? DBNull.Value);
            command.Parameters.AddWithValue("@WeekDay", serie.WeekDay ?? "");
            command.Parameters.AddWithValue("@MonthDay", serie.MonthDay);
            command.Parameters.AddWithValue("@Month", serie.Month);
            await command.ExecuteNonQueryAsync(token);
        }
        public static async Task DeleteSeriesAsync(int seriesId)
        {
            var connection = new SqliteConnection("Data Source=serientermine.db");
            connection.Open();

            var sqlDelete = connection.CreateCommand();
            sqlDelete.CommandText = @"
                            DELETE FROM Series
                            WHERE Id = @Id
                            ";

            sqlDelete.Parameters.AddWithValue("@Id", seriesId);
            sqlDelete.ExecuteNonQuery();
            connection.Close();

            await Task.CompletedTask;
        }
    }
}
