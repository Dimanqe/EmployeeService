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
CREATE TABLE Passports (
    Id INT PRIMARY KEY IDENTITY,
    Type NVARCHAR(20),
    Number NVARCHAR(20)
);
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Surname NVARCHAR(100),
    Phone NVARCHAR(20),
    PassportId INT,
    CompanyId INT,
    DepartmentId INT, 
    FOREIGN KEY (PassportId) REFERENCES Passports(Id),
    FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),  
);


