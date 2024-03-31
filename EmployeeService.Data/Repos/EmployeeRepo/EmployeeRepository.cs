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

        public async Task<int> Create(Employee employee, Passport passport)
        {
            try
            {
                OpenConnection();

                const string passportSql = @"
            INSERT INTO Passports (Type, Number)
            VALUES (@Type, @Number);
            SELECT SCOPE_IDENTITY();";         

                int passportId = await _dbConnection.ExecuteScalarAsync<int>(passportSql, new
                {
                    passport.Type,
                    passport.Number
                });

                const string employeeSql = @"
            INSERT INTO Employees (Name, Surname, Phone, PassportId, CompanyId, DepartmentId)
            VALUES (@Name, @Surname, @Phone, @PassportId, @CompanyId, @DepartmentId)";

                await _dbConnection.ExecuteAsync(employeeSql, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    PassportId = passportId,       
                    employee.CompanyId,
                    employee.DepartmentId,
                });

                return passportId;     
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
                        E.PassportId,
                        E.DepartmentId,
                        E.CompanyId,
                        P.Id AS Id,
                        P.Type AS Type,
                        P.Number AS Number,
                        D.Id AS Id,
                        D.Name AS Name,
                        D.Phone AS Phone,
                        C.Id AS Id,
                        C.Name AS Name,
                        C.Address AS Address,
                        C.Phone AS Phone
                    FROM Employees AS E
                    LEFT JOIN Passports AS P ON E.CompanyId = P.Id
                    LEFT JOIN Departments AS D ON E.DepartmentId = D.Id
                    LEFT JOIN Companies AS C ON E.CompanyId = C.Id";

                var employees = await _dbConnection.QueryAsync<Employee,Passport, Department, Company, Employee>(
                    sql,
                    (employee,passport, department, company) =>
                    {
                        employee.Passport = passport;
                        employee.Department = department;
                        employee.Company = company;
                        return employee;
                    },
                    splitOn: "Id,Id,Id"
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

        public async Task Update(Employee employee,Passport passport, int id)
        {
            try
            {
                OpenConnection();
                const string employeeSql = @"
                    UPDATE Employees 
                    SET 
                        Name = COALESCE(@Name, Name), 
                        Surname = COALESCE(@Surname, Surname), 
                        Phone = COALESCE(@Phone, Phone), 
                        CompanyId = CASE WHEN @CompanyId IS NULL THEN CompanyId ELSE @CompanyId END,
                        DepartmentId = CASE WHEN @DepartmentId IS NULL THEN DepartmentId ELSE @DepartmentId END                                                       
                    WHERE Id = @Id";

                const string passportSql = @"
                    UPDATE Passports 
                    SET 
                        Type = COALESCE(@Type, Type), 
                        Number = COALESCE(@Number, Number)                                                     
                    WHERE Id = @Id";

                await _dbConnection.ExecuteAsync(employeeSql, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,
                    employee.DepartmentId,
                    Id = id
                });
                await _dbConnection.ExecuteAsync(passportSql, new
                {
                    passport.Type,
                    passport.Number,
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
                        E.PassportId,
                        E.DepartmentId,
                        E.CompanyId,
                        P.Id AS Id,
                        P.Type AS Type,
                        P.Number AS Number,
                        D.Id AS Id,
                        D.Name AS Name,
                        D.Phone AS Phone,
                        C.Id AS Id,
                        C.Name AS Name,
                        C.Address AS Address,
                        C.Phone AS Phone
                    FROM Employees AS E
                    LEFT JOIN Passports AS P ON E.CompanyId = P.Id
                    LEFT JOIN Departments AS D ON E.DepartmentId = D.Id
                    LEFT JOIN Companies AS C ON E.CompanyId = C.Id
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
