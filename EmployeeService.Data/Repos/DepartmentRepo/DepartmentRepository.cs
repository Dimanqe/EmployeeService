using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _dbConnection;

        public DepartmentRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(Department department)
        {
            try
            {
                OpenConnection();
                const string companySql = @"
                    INSERT INTO Departments (Name, Phone) 
                    VALUES (@Name, @Phone)";

                await _dbConnection.ExecuteAsync(companySql, new
                {
                    department.Name,
                    department.Phone
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the department.", ex);
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
                    DELETE FROM Departments WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the department.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<Department> Get(int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Name, Phone FROM Departments WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Department>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the department.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Department>> GetAll()
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Name, Phone FROM Departments";

                return (await _dbConnection.QueryAsync<Department>(sql)).AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all departments.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task Update(Department department, int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    UPDATE Departments 
                    SET 
                        Name = COALESCE(@Name, Name),                         
                        Phone = COALESCE(@Phone, Phone)                                                       
                    WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new
                {
                    department.Name,
                    department.Phone,
                    Id = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the department.", ex);
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