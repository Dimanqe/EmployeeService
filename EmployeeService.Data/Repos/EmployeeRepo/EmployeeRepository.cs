using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EmployeeService.Data.Models;

namespace EmployeeService.Data.Repos.EmployeeRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
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

        public async Task Create(Employee employee)
        {
            try
            {
                OpenConnection();
                const string employeeSql = @"
                    INSERT INTO Employees (Name, Surname, Phone, CompanyId, DepartmentId, PassportType, PassportNumber)
                    VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId, @PassportType, @PassportNumber)";

                await _dbConnection.ExecuteAsync(employeeSql, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,

                    employee.DepartmentId,
                    employee.PassportType,
                    employee.PassportNumber
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the employee.", ex);
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
                    DELETE FROM Employees WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the employee.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<Employee> GetById(int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT Id, Name, Phone, CompanyId, 
                    DepartmentName AS Name, DepartmentPhone AS Phone, 
                    PassportType AS Type, PassportNumber AS Number
                    FROM Employees
                    WHERE Id = @Id";

                return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the employee by Id.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT 
                        E.Id,
                        E.Name,
                        E.Surname,
                        E.Phone,
                        E.CompanyId,
                        E.DepartmentId,
                        E.PassportType,
                        E.PassportNumber,
                        D.Id AS Id,
                        D.Name AS Name,
                        D.Phone AS Phone,
                        C.Id AS Id,
                        C.Name AS Name,
                        C.Address AS Address,
                        C.Phone AS Phone
                    FROM Employees AS E
                    LEFT JOIN Companies AS C ON E.CompanyId = C.Id
                    LEFT JOIN Departments AS D ON E.DepartmentId = D.Id";

                var employees = await _dbConnection.QueryAsync<Employee, Department, Company, Employee>(
                    sql,
                    (employee, department, company) =>
                    {
                        employee.Department = department;
                        employee.Company = company;
                        return employee;
                    },
                    splitOn: "Id,Id"
                );

                return employees.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all employees.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task Update(Employee employee, int id)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    UPDATE Employees 
                    SET 
                        Name = COALESCE(@Name, Name), 
                        Surname = COALESCE(@Surname, Surname), 
                        Phone = COALESCE(@Phone, Phone), 
                        CompanyId = CASE WHEN @CompanyId IS NULL THEN CompanyId ELSE @CompanyId END,
                        DepartmentId = CASE WHEN @DepartmentId IS NULL THEN DepartmentId ELSE @DepartmentId END,
                        PassportType = COALESCE(@PassportType, PassportType), 
                        PassportNumber = COALESCE(@PassportNumber, PassportNumber)                                
                    WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(sql, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,
                    employee.DepartmentId,
                    employee.PassportType,
                    employee.PassportNumber,
                    Id = id
                });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the employee.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<int> GetId(Employee employee)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT ID FROM Employees WHERE Name = @Name AND Phone = @Phone";

                return await _dbConnection.QueryFirstOrDefaultAsync<int>(sql, new { employee.Name, employee.Phone });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the employee Id.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task<List<Employee>> GetByCompanyId(int companyId)
        {
            try
            {
                OpenConnection();
                const string sql = @"
                    SELECT 
                        E.Id,
                        E.Name,
                        E.Surname,
                        E.Phone,
                        E.CompanyId,
                        E.DepartmentId,
                        E.PassportType,
                        E.PassportNumber,
                        D.Id AS Id,
                        D.Name AS Name,
                        D.Phone AS Phone,
                        C.Id AS Id,
                        C.Name AS Name,
                        C.Address AS Address,
                        C.Phone AS Phone
                    FROM Employees AS E
                    LEFT JOIN Companies AS C ON E.CompanyId = C.Id
                    LEFT JOIN Departments AS D ON E.DepartmentId = D.Id
                    WHERE E.CompanyId = @CompanyId";

                var employees = await _dbConnection.QueryAsync<Employee, Department, Company, Employee>(
                    sql,
                    (employee, department, company) =>
                    {
                        employee.Department = department;
                        employee.Company = company;
                        return employee;
                    },
                    new { CompanyId = companyId },
                    splitOn: "Id,Id"
                );

                return employees.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving employees by CompanyId.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
