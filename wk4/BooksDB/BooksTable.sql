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

-- Create a Books Table
CREATE TABLE Books
(
	Title VARCHAR(250) PRIMARY KEY,
	Author VARCHAR(100) FOREIGN KEY REFERENCES Authors(Author) NOT NULL,
	Pages INT NOT NULL,
	Thickness VARCHAR(10) NOT NULL,
	GenreID INT NOT NULL,
	PublisherID INT NOT NULL,
);

--Create the Format-Price Table *Composite KEY!*
CREATE TABLE FormatPrice
(
	Title VARCHAR(250) NOT NULL,
	PrintFormat VARCHAR(50) NOT NULL,
	Price MONEY NOT NULL,
	PRIMARY KEY (Title, PrintFormat)
);


--Introducing ALTER - change something about a table that is already in the db.
--VERB NOUN <NAME>, VERB <NAME> <TYPE> <Mods>
ALTER TABLE Genres ADD Genre VARCHAR(100) NOT NULL;

--DROP to delete/remove something from a table/database
--VERB NOUN <NAME>, VERB NOUN <NAME>
ALTER TABLE Genres DROP COLUMN Genre;

DROP TABLE Genres;

--Modify a table to REMOVE the NOT NULL property from a field.
ALTER TABLE Books ALTER COLUMN Pages INT NOT NULL;

--Create a Primary Key as an alteration
ALTER TABLE FormatPrice ADD PRIMARY KEY (Title, PrintFormat);

--Rename a table?
--ALTER TABLE Genres CHANGE COLUMN Genre TO Genre2;


--Create a Foreign Key as an alteration
ALTER TABLE Books ADD FOREIGN KEY (Author) REFERENCES Authors(Author);
--Verb Noun <TableName> VERB NOUN <ColumnNAME> "REFERENCES" <FTable>(<FColumn>)

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

--SELECT WHERE

SELECT * FROM Books WHERE Author='Chad Russell';

SELECT * FROM Books WHERE Thickness!='Thick';


--Combine tables with a "JOIN"