CREATE TABLE Companies (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),    
    Address NVARCHAR(100),
    Phone NVARCHAR(20)
);
CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Phone NVARCHAR(20)
);
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Surname NVARCHAR(100),
    Phone NVARCHAR(20),
    CompanyId INT,
    DepartmentId INT,
    PassportType NVARCHAR(20),
    PassportNumber NVARCHAR(20),
    FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),  
);


