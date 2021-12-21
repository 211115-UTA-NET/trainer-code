--Create Department Table
CREATE TABLE Department
(
    ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    "Name" NVARCHAR(255) NOT NULL,
    "Location" NVARCHAR(255) NOT NULL 
);

--Create Employee Table
CREATE TABLE Employee
(
    ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    SSN INT NOT NULL UNIQUE,
    DeptID INT NOT NULL FOREIGN KEY REFERENCES Department(ID)
);

--Create EmpDetails Table
CREATE TABLE EmpDetails
(
    ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employee(ID),
    Salary MONEY NOT NULL,
    Address1 NVARCHAR(255) NOT NULL,
    Address2 NVARCHAR(255) NULL,
    City NVARCHAR(255) NOT NULL,
    "State" NVARCHAR(255) NOT NULL,
    Country NVARCHAR(255) NOT NULL
);


--Fill Department
INSERT Department
    ("Name", "Location")
VALUES
    ('Sales', 'New York'),
    ('Publishing', 'Orlando'),
    ('Manufacturing', 'Paris');

--Fill Employee
INSERT Employee
    (FirstName, LastName, SSN, DeptID)
VALUES
    ('John', 'Doe', 123456789, 1),
    ('Jane', 'Smith', 123456790, 2),
    ('Bruce', 'Wayne', 123456791, 3);

--Fill EmpDetails
INSERT EmpDetails
    (EmployeeID, Salary, Address1, Address2, City, "State", Country)
VALUES
    (1, 50000, '1 Rhode St', 'Apt 3A', 'Townsville', 'New Jersey', 'USA'),
    (2, 60000, '2 Street Rd', 'PO Box 4', 'Villeburg', 'Florida', 'USA'),
    (3, 70000, '3 Boulevard Ave', '' , 'Paris', 'Ile-de-France', 'France');


--Queries

--Add employee Tina Smith
INSERT Employee
    (FirstName, LastName, SSN, DeptID)
VALUES
    ('Tina', 'Smith', 123456792, 1);

--Add department Marketing
INSERT Department
    ("Name", "Location")
VALUES
    ('Marketing', 'Moscow');

--List all Employees in Marketing
SELECT Employee.FirstName + Employee.LastName AS EmployeeName, Department.Name
FROM Employee
INNER JOIN Department ON Department.ID=Employee.DeptID
WHERE Department.Name='Marketing'
ORDER BY EmployeeName;

--Report total salary of Marketing department
SELECT SUM(Salary)
FROM EmpDetails
FULL OUTER JOIN Employee ON Employee.ID=EmpDetails.EmployeeID
FULL OUTER JOIN Department ON Department.ID=Employee.DeptID
WHERE Department.Name='Marketing'

--Report total employees by department
SELECT Department.Name
FROM Department
JOIN Employee ON Department.ID=Employee.DeptID

--Increase salary of Tina Smith to 90000
UPDATE EmpDetails
SET Salary=90000
Where FirstName='Tina' AND LastName='Smith';