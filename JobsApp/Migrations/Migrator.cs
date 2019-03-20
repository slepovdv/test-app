using Dapper;
using JobsApp.Services.DatabaseService;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace JobsApp.Migrations
{
    public class Migrator
    {
        private readonly DatabaseServiceConfig _databaseServiceSettings;
        private readonly ILogger<Migrator> _logger;

        public Migrator(ILogger<Migrator> logger, DatabaseServiceConfig databaseServiceConfig)
        {
            _logger = logger;
            _databaseServiceSettings = databaseServiceConfig;
        }

        public void Run()
        {
            using (var connection = OpenConnection())
            {
                CreateMigrationsTable(connection);
                var appliedMigrations = GetAppliedMigrations(connection);
                var allMigrations = GetAllMigrations();
                foreach(var migration in allMigrations)
                {
                    var number = migration.GetNumber();
                    if (appliedMigrations.Contains(number))
                    {
                        continue;
                    }
                    try
                    {
                        var query = migration.Up();
                        var sql = $"BEGIN; {query}; insert into Migrations Values (@number); COMMIT;";
                        connection.Execute(sql, new { number });
                        _logger.LogInformation($"Миграция {number} накачена");
                    }

                    catch (NpgsqlException e)
                    {
                        _logger.LogError($"Не удалось накатить миграцию № {number}", e);
                    }
                }
            }
        }

        private Migration[] GetAllMigrations()
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly.DefinedTypes
             .Where(t => t.IsClass && (t.BaseType == typeof(Migration) || t.BaseType.BaseType == typeof(Migration)))
             .Where(t => !string.IsNullOrEmpty(t.Namespace))
             .Select(m => (Migration)Activator.CreateInstance(m))
             .ToArray();
        }

        private void CreateMigrationsTable(IDbConnection connection)
        {
            _logger.LogInformation("Создаем таблицу для миграций");
            try
            {
                var sql = "CREATE TABLE IF NOT EXISTS Migrations (MigrationId integer PRIMARY KEY)";
                connection.Execute(sql);
            }
            catch(NpgsqlException e)
            {
                _logger.LogError("Не удалось создать таблицу для миграций", e);
            }
        }

        public IEnumerable<int> GetAppliedMigrations(IDbConnection connection)
        {
            return connection.Query<int>("select MigrationId from Migrations");
        }

        private IDbConnection OpenConnection()
        {
            var conn = new NpgsqlConnection(_databaseServiceSettings.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
