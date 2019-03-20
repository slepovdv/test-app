using Dapper;
using JobsApp.Models;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApp.Services.DatabaseService.Impl
{
    public class DatabaseService : IDatabaseService
    {
        private ILogger<DatabaseService> _logger;
        private DatabaseServiceConfig _databaseServiceSettings;

        public DatabaseService(DatabaseServiceConfig databaseServiceSettings, ILogger<DatabaseService> logger)
        {
            _logger = logger;
            _databaseServiceSettings = databaseServiceSettings;
        }

        public async Task<VacancyModel[]> Get(string name, int salaryFrom)
        {
            var filter = GetFilter(name, salaryFrom);
            try
            {
                using (var conn = OpenConnection())
                {
                    var sql = $@"SELECT Id, Name, SalaryFrom, SalaryTo, Organization, ContactFullName, Address, Phone, Type, Description, Requierments, Responsibility
                            FROM vacancies
                            WHERE {filter}";
                    var items = await conn.QueryAsync<VacancyModel>(sql, new
                    {
                        name  = string.IsNullOrEmpty(name)? string.Empty : $"%{name.ToUpper()}%",
                        salaryFrom
                    });
                    return items.ToArray();
                }
            }
            catch(NpgsqlException e)
            {
                _logger.LogError("Произошла ошибка при чтении вакансий из БД", e);
                return new VacancyModel[0];
            }
        }


        public async Task<bool> HaveVacancies()
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    var sql = $@"SELECT count(*) FROM vacancies";
                    var count = await conn.ExecuteScalarAsync<int>(sql);
                    return count > 0;
                }
            }
            catch (NpgsqlException e)
            {
                _logger.LogError("Произошла ошибка при чтении вакансий из БД", e);
                return false;
            }
        }

        private string GetFilter(string name, int salaryFrom)
        {
            return $"{GetNameFilter(name)} AND {GetSalaryFrom(salaryFrom)}";
        }

        private string GetNameFilter(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "TRUE";
            }
            return "UPPER(Name) like @name";
        }

        private string GetSalaryFrom(int salaryFrom)
        {
            if (salaryFrom == 0)
            {
                return "TRUE";
            }
            return "salaryFrom >= @salaryFrom";
        }

        public async Task Insert(VacancyModel[] vacancies)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    var transaction = conn.BeginTransaction();
                    foreach (var vacancy in vacancies)
                    {
                        await Insert(conn, vacancy);
                    }
                    transaction.Commit();
                }
            }
            catch (NpgsqlException e)
            {
                _logger.LogError("Произошла ошибка при записи вакансий из БД", e);
            }
        }

        private async Task Insert(IDbConnection connection, VacancyModel vacancy)
        {
            var sql = @"
                    INSERT INTO vacancies(Id, Name, SalaryFrom, SalaryTo, Organization, ContactFullName, Address, Phone, Type, Description, Requierments, Responsibility)
                    VALUES (@Id, @Name, @SalaryFrom, @SalaryTo, @Organization, @ContactFullName, @Address, @Phone, @Type, @Description, @Requierments, @Responsibility)";
            await connection.ExecuteAsync(sql, vacancy);
        }

        private IDbConnection OpenConnection()
        {
            var conn = new NpgsqlConnection(_databaseServiceSettings.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
