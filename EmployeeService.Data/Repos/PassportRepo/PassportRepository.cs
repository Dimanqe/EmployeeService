using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.PassportRepo
{
    public class PassportRepository : IPassportRepository
    {
        private readonly IDbConnection _dbConnection;

        public PassportRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(Passport passport)
        {
            try
            {
                OpenConnection();
                const string companySql = @"
                    INSERT INTO Passports (Type, Number) 
                    VALUES (@Type, @Number)";

                await _dbConnection.ExecuteAsync(companySql, new
                {
                    passport.Type,
                    passport.Number
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the Passport.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    DELETE FROM Passports WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the Passport.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Passport>> GetAll()
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Type, Number FROM Passports";

                return (await _dbConnection.QueryAsync<Passport>(sql)).AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all Passports.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task Update(Passport passport, int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    UPDATE Passports 
                    SET 
                        Type = COALESCE(@Type, Type),                         
                        Number = COALESCE(@Number, Number)                                                       
                    WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new
                {
                    passport.Type,
                    passport.Number,
                    Id = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the Passport.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void OpenConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
        }

        private void CloseConnection()
        {
            if (_dbConnection.State != ConnectionState.Closed)
                _dbConnection.Close();
        }

        public async Task<Passport> Get(int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Type, Number FROM Passports WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Passport>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the Passport.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}