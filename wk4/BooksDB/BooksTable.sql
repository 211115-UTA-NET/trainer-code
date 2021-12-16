--Create an Authors Table
CREATE TABLE Authors
(
	Author VARCHAR(100) PRIMARY KEY,
	AuthorNationality VARCHAR(100) NOT NULL
);

--Create a Genres Table
CREATE TABLE Genres
(
	ID INT PRIMARY KEY,
	Genre VARCHAR(100) NOT NULL
);

-- VARCHAR, CHAR use the encoding defined by the database's "collation"
--   the collation (of a DB, of a particular query) defines
--   localization type things like - currency symbol, period vs comma, case sensitivity, char encoding
-- NCHAR, NVARCHAR accept any Unicode character, like strings in .NET.

-- Create a Books Table
SELECT * FROM Books;
--DROP TABLE Books;
CREATE TABLE Books
(
	Title NVARCHAR(250) PRIMARY KEY,
	Author VARCHAR(100) FOREIGN KEY REFERENCES Authors(Author) NOT NULL,
	Pages INT NOT NULL,
	Thickness VARCHAR(10) NOT NULL,
	GenreID INT FOREIGN KEY REFERENCES Genres(ID) NOT NULL,
	PublisherID INT NOT NULL,
    DateModified DATETIMEOFFSET NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
    BookBizId AS (SUBSTRING(Title,1,3) + SUBSTRING(Author,1,3) + '000') PERSISTED,   -- computed column
    CONSTRAINT CK_Pages_Positive CHECK (Pages > 0)
);

-- non-persisted computed columns: are not stored in the DB, are recomputed every query
-- persisted computed columns: are stored in the DB, are recomputed every update/insert

--Create the Format-Price Table *Composite KEY!*
--DROP TABLE FormatPrice;
CREATE TABLE FormatPrice
(
	Title NVARCHAR(250) FOREIGN KEY REFERENCES Books(Title) NOT NULL,
	PrintFormat VARCHAR(50) NOT NULL,
	Price MONEY NOT NULL,
	CONSTRAINT PK_FormatPrice PRIMARY KEY (Title, PrintFormat),
);


--Introducing ALTER - change something about a table that is already in the db.
--VERB NOUN <NAME>, VERB <NAME> <TYPE> <Mods>
ALTER TABLE Genres ADD Genre VARCHAR(100) NOT NULL;

--Modify a table to REMOVE the NOT NULL property from a field.
ALTER TABLE Books ALTER COLUMN Pages INT NOT NULL;

--Create a Primary Key as an alteration
ALTER TABLE FormatPrice ADD PRIMARY KEY (Title, PrintFormat);

--DROP to delete/remove something from a table/database
--VERB NOUN <NAME>, VERB NOUN <NAME>
ALTER TABLE Genres DROP COLUMN Genre;
DROP TABLE Genres;


--Rename a table?
--ALTER TABLE Genres CHANGE COLUMN Genre TO Genre2;

--Create a Foreign Key as an alteration
ALTER TABLE Books ADD FOREIGN KEY (Author) REFERENCES Authors(Author);
--Verb Noun <TableName> VERB NOUN <ColumnNAME> "REFERENCES" <FTable>(<FColumn>)

-- giving constraints names so they can be more conveniently altered or dropped later
--ALTER TABLE Books ADD CONSTRAINT FK_Author FOREIGN KEY (Author) REFERENCES Authors(Author);
--ALTER TABLE Books DROP CONSTRAINT FK_Author;


ALTER TABLE Books ADD FOREIGN KEY (GenreID) REFERENCES Genres(ID);

ALTER TABLE FormatPrice ADD FOREIGN KEY (Title) REFERENCES Books(Title);

--DML Data Manipulation Language
--Insert - place data in a table

--When inserting, order matters. Add/INSERT data into your tables from the foreign key back.
--Any table that includes a foreign key to another table must have a valid target to reference before it can be inserted.

INSERT Genres
	(ID,Genre)
VALUES
	(1,'Tutorial'),
	(2,'Popular Science');

INSERT Authors
	(Author, AuthorNationality)
VALUES
	('Chad Russell', 'American'),
	('E.F.Codd', 'British');

INSERT Books
	(Title, Author, Pages, Thickness, GenreID, PublisherID)
VALUES
	('Beginning MySQL Database Design and Optimization', 'Chad Russell', 520, 'Thick', 1, 1),
	('The Relational Model for Database Management: Version 2', 'E.F.Codd', 538, 'Thick', 2, 2),
	('Chads New Book', 'Chad Russell', 20, 'Thin', 2, 1);

INSERT FormatPrice
	(Title, PrintFormat, Price)
VALUES
	('Beginning MySQL Database Design and Optimization', 'Hardcover', 49.99),
	('Beginning MySQL Database Design and Optimization',  'E-book',	22.34),
	('The Relational Model for Database Management: Version 2', 'E-book', 13.88),
	('The Relational Model for Database Management: Version 2', 'Paperback', 39.99);


--DML/DQL Data Query Language
--SELECT - requests information from a table

--VERB <TARGET/What/filter> FROM <Table>
SELECT * FROM Books;

SELECT Author FROM Books;

SELECT Title, Author, Pages FROM Books;

--SELECT WHERE - add filter to a SELECT
SELECT * FROM Books WHERE Author='Chad Russell';

SELECT * FROM Books WHERE Thickness!='Thick';



--Return more data from other tables with a "JOIN"

--Inner Join
--Selects all rows from both tables, as long as there is a batch between the columns.
--If there are records in "Table A" that do not have a matching record in "Table B", they are not shown

-- SELECT column_name
-- FROM Table_A
-- INNER JOIN Table_B
-- ON Table_A.column_name = Table_B.column_Name;


-- LEFT JOIN
-- Returns all records from the "Left" table, even if there are not matching records in the "Right" table

-- SELECT column_name
-- FROM Table_A
-- LEFT JOIN Table_B
-- ON Table_A.column_name = Table_B.column_Name;


-- RIGHT JOIN
-- Returns all records from the "Right" table, even if there are not matching records in the "Left" table.

-- niSELECT column_name
-- FROM Table_A
-- LEFT JOIN Table_B
-- ON Table_A.column_name = Table_B.column_Name;


-- FULL JOIN
-- Returns all records when there is a match in "Left" or "Right" table.

-- SELECT column_name
-- FROM Table_A
-- FULL OUTER JOIN Table_B
-- ON Table_A.column_name = Table_B.column_Name;


-- SELF JOIN
-- Returns a table that has been formed by joining a table with itself.

-- SELECT A.columnA, B.columnB, A.columnC
-- FROM TableA A, TableA B
-- WHERE A.columnA <> B.columnB
-- And A.columnC = B.columnC;


--UNIQUE - every value in the specified column is different
--NOT NULL - ever record (row) must have the specified column filled
--NULL - NULL is allowed as a possible value (and is also the default)
--DEFAULT (expr) - use a different default instead of NULL

--UNIQUE and NOT NULL are part of a columns eligibility to be a PRIMARY KEY

--IDENTITY - the SQL Server way to automatically increment the next record(row) in the specified column

--CREATE TABLE People
-- (
-- 	ID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
--  LastName VARCHAR(255) NOT NULL,
--  FirstName VARCHAR(255) NOT NULL
-- )

--IDENTITY(_,_) - start counting at, increment by

-- INSERT People
-- (LastName, FirstName)
-- VALUES
-- ('Schweers', 'Stefan'),
-- ('Cicio', 'Edward');

--DATES
--DATE, DATETIME, SMALLDATE, TIMESTAMP
--YYYY-MM-DD, YYYY-MM-DD HH:mm:ss, YYYY-MM-DD HH:mm:ss, unique



--WHERE... AND/OR/NOT
--and/or/not allows us o refine the results of a select where statement

-- SELECT column1, column2
-- FROM TableA
-- WHERE Condition1 AND Condition2 OR Condition3
-- WHERE Condition1 OR Condition2 AND Condition3
-- WHERE NOT Condition1 AND Condition2



--ORDER - gives us a way to sort the results of a query.
--ASC|DESC return order ascending or descending

SELECT * FROM Books
ORDER BY Author , Pages DESC;

-- VIEWS - like "computed tables" - the whole table is "fake"
GO
CREATE VIEW AllBookData WITH SCHEMABINDING AS
    SELECT B.Title, B.Author + '!', A.AuthorNationality, B.Pages, B.Thickness,
        B.GenreID, G.Genre, B.PublisherID, B.DateModified, B.BookBizId, FP.PrintFormat, FP.Price
    FROM Books AS B
    INNER JOIN FormatPrice AS FP ON B.Title = FP.Title
    INNER JOIN Genres AS G ON G.ID = B.GenreID
    INNER JOIN Authors AS A ON A.Author = B.Author;
GO
-- enables a more convenient denormalized layer of abstraction on top of the normalized tables
SELECT Title, Price FROM AllBookData;
-- views with SCHEMABINDING maintain a lock on the definition of anything they depend on
UPDATE AllBookData SET PublisherID = 0 WHERE 1 = 0;
-- view data is not stored separately or at all; the underlying SELECT is run every time you access the view

-- VARIABLES
-- within one batch, you can have temporary variables.
-- they disappear as soon as the batch is done

-- TABLE is a valid data type for variables

DECLARE @id INT;
SELECT @id = MAX(ID) + 1 FROM Genres;
--SET @id = (SELECT MAX(ID) + 1 FROM Genres); -- equivalent
INSERT INTO Genres (ID, Genre) VALUES
    (@id, 'Adventure');
INSERT INTO Books (GenreID, Title) VALUES
    (@id, 'Adventure Book');
SELECT * FROM Genres;

-- FUNCTIONS
-- define your own functions in the database
-- (must be read-only! no update, insert, create, etc.)

ALTER TABLE Books DROP COLUMN Thickness;
GO
CREATE OR ALTER FUNCTION ThicknessFromPages(@pages INT) RETURNS VARCHAR(50) AS
BEGIN
    -- can do procedural stuff like conditional, loops, as well as SELECT statements
    IF (@pages < 50) RETURN 'Very short';
    IF (@pages < 100) RETURN 'Short';
    IF (@pages < 250) RETURN 'Medium';
    RETURN 'Long';
END
GO

--CREATE SCHEMA BookApp;
-- schema is a object in the database that acts like a namespace
-- the default schema is "dbo"
-- apart from just organizing your stuff, schemas are good scopes of permission/authorization
--  ... but also, by another definition, "schema" means your overall database structure/design.

ALTER TABLE Books ADD Thickness AS dbo.ThicknessFromPages(Pages);

SELECT * FROM Books;

-- PROCEDURES

-- procedures are like functions in that they encapsulate some logic as a unit you can call with
-- parameters
--     functions: readonly to the db (no side effects), but you can use them inside other statements (like SELECT)
--     procedures: can do anything, but can only be called by themselves in their own statement (EXECUTE)

--SELECT Pages, dbo.ThicknessFromPages(Pages) FROM Books WHERE ;

GO
CREATE OR ALTER PROCEDURE UpdateBooksDateModified(@maxpages INT, @rowsmodified INT OUTPUT) AS
BEGIN
    SELECT @rowsmodified = COUNT(*) FROM Books WHERE Pages < @maxpages;
    -- could have IF, loop
    -- we even have TRY, CATCH, THROW
    UPDATE Books
    SET DateModified = SYSDATETIMEOFFSET()
    WHERE Pages < @maxpages;
END
GO

DECLARE @rows INT;
EXECUTE UpdateBooksDateModified 100, @rows OUTPUT;
SELECT @rows;
SELECT * FROM Books;

-- TRIGGERS

-- triggers are blocks of statements that can run AFTER or INSTEAD OF
-- an INSERT, UPDATE, or DELETE on a particular table.

-- in C#, properties let you interject some validation or transforming of data in the setter
-- this is a little like that

-- equiv of the check constraint on books.pages:
GO
--DROP TRIGGER Books_PositivePages;
CREATE OR ALTER TRIGGER Books_PositivePages ON Books
INSTEAD OF INSERT -- should also cover the update case
AS 
BEGIN
    --PRINT 'insertion not allowed';
    -- inside a trigger you have access to two table-valued variables: Inserted & Deleted
    -- these show the inserted rows, or the deleted rows
    -- or, if it's an update, Deleted will have the old versions of the rows;
    --   and Inserted will have the new versions
    IF (EXISTS(SELECT Pages FROM Inserted WHERE Pages <= 0))
    BEGIN;
        THROW 50000, 'pages must be greater than 0', 1;
    END
    INSERT INTO Books
    SELECT * FROM Inserted; -- something's wrong here, not sure what
END

GO
INSERT INTO Books
	(Title, Author, Pages, GenreID, PublisherID)
VALUES
	('Asdf', 'Chad Russell', -520, 1, 1);
SELECT * FROM Books;

GO
CREATE OR ALTER TRIGGER Books_DateModified ON Books
AFTER UPDATE
AS 
BEGIN
    UPDATE Books
    SET DateModified = SYSDATETIMEOFFSET()
    WHERE Title IN (SELECT Title FROM Inserted);
END

SELECT * FROM Books;

UPDATE Books SET PublisherID = 3 WHERE PublisherID = 2;

INSERT INTO Books (Title, Pages) VALUES ('asdf', 20);