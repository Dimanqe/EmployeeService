using System;
using System.Data;
using System.IO;
using Dapper;

namespace EmployeeService.Data
{
    public class DatabaseInitializer
    {
        private readonly IDbConnection _dbConnection;
        private readonly string _scriptFilePath;
        private readonly string _tablesMockDataFillScript;

        public DatabaseInitializer(IDbConnection dbConnection, string scriptFilePath, string tablesMockDataFillScript)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _scriptFilePath = scriptFilePath ?? throw new ArgumentNullException(nameof(scriptFilePath));
            _tablesMockDataFillScript = tablesMockDataFillScript ??
                                        throw new ArgumentNullException(nameof(tablesMockDataFillScript));
        }

        public void InitializeDatabase()
        {
            if (!File.Exists(_scriptFilePath))
                throw new FileNotFoundException($"Script file not found at path: {_scriptFilePath}");
            _dbConnection.Open();

            var checkDatabaseExistsQuery = "SELECT COUNT(*) FROM sys.databases WHERE name='EmployeeService'";
            var databaseCount = _dbConnection.ExecuteScalar<int>(checkDatabaseExistsQuery);

            if (databaseCount == 0)
            {
                var createDatabaseScript = "CREATE DATABASE EmployeeService";
                _dbConnection.Execute(createDatabaseScript);
                _dbConnection.ChangeDatabase("EmployeeService");
                var createTablesScript = File.ReadAllText(_scriptFilePath);
                var tablesMockDataFillScript = File.ReadAllText(_tablesMockDataFillScript);
                _dbConnection.Execute(createTablesScript);
                _dbConnection.Execute(tablesMockDataFillScript);
            }
        }
    }
}