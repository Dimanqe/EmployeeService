using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.CompanyRepo
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _dbConnection;

        public CompanyRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(Company company)
        {
            try
            {
                OpenConnection();
                const string companySql = @"
                    INSERT INTO Companies (Name, Address, Phone) 
                    VALUES (@Name, @Address, @Phone)";

                await _dbConnection.ExecuteAsync(companySql, new
                {
                    company.Name,
                    company.Address,
                    company.Phone
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the company.", ex);
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
                    DELETE FROM Companies WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the company.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<Company> Get(int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Name, Address, Phone FROM Companies WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Company>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the company.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Company>> GetAll()
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Name, Address, Phone FROM Companies";

                return (await _dbConnection.QueryAsync<Company>(sql)).AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all companies.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task Update(Company company, int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    UPDATE Companies 
                    SET 
                        Name = COALESCE(@Name, Name), 
                        Address = COALESCE(@Address, Address), 
                        Phone = COALESCE(@Phone, Phone)                                                       
                    WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new
                {
                    company.Name,
                    company.Address,
                    company.Phone,
                    Id = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the company.", ex);
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
    }
}