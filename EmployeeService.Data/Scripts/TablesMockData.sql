INSERT INTO Companies (Name, Address, Phone)
VALUES
    ('Bosch', 'Address A', '1234567890'),
    ('Siemens', 'Address B', '0987654321'),
    ('ABB', 'Address C', '1112223333');

INSERT INTO Departments (Name, Phone)
VALUES
    ('Sales', '5556667777'),
    ('Accounting', '8889990000'),
    ('Development', '1231231234');

INSERT INTO Passports (Type, Number)
VALUES
    ('Type 1', 'AB123456'),
    ('Type 2', 'CD789012'),
    ('Type 3', 'EF345678');

INSERT INTO Employees (Name, Surname, Phone, PassportId, CompanyId, DepartmentId)
VALUES
    ('John', 'Doe', '123456789', 1, 1, 2),
    ('Alice', 'Smith', '987654321', 2, 2, 3),
    ('Bob', 'Johnson', '111222333', 3, 3, 1);