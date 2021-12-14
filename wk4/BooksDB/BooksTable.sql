-- Create a Books Table
CREATE TABLE Books
(
	Title VARCHAR(250) PRIMARY KEY,
	Author VARCHAR(100) NOT NULL,
	Pages INT NOT NULL,
	Thickness VARCHAR(10) NOT NULL,
	GenreID INT NOT NULL,
	PublisherID INT NOT NULL,
);

--Create an Authors Table
CREATE TABLE Authors
(
	Author VARCHAR(100) PRIMARY KEY,
	AuthorNationality VARCHAR(100) NOT NULL
);

--Create a Genres Table
CREATE TABLE Genres
(
	ID INT PRIMARY KEY
);


--Introducing ALTER - change something about a table that is already in the db.
--VERB NOUN <NAME>, VERB <NAME> <TYPE> <Mods>
ALTER TABLE Genres ADD Genre VARCHAR(100) NOT NULL;


--DROP to delete/remove something from a table/database
--VERB NOUN <NAME>, VERB NOUN <NAME>
ALTER TABLE Genres DROP COLUMN Genre;

DROP TABLE Genres;

CREATE TABLE Genres
(
	ID INT PRIMARY KEY,
	Genre VARCHAR(100) NOT NULL
);


--Modify a table to REMOVE the NOT NULL property from a field.
ALTER TABLE Books ALTER COLUMN Pages INT NOT NULL;


--Create the Format-Price Table *Composite KEY!*
CREATE TABLE FormatPrice
(
	Title VARCHAR(250) NOT NULL,
	PrintFormat VARCHAR(50) NOT NULL,
	Price MONEY NOT NULL,
	PRIMARY KEY (Title, PrintFormat)
);

--Create a Primary Key as an alteration
ALTER TABLE FormatPrice ADD PRIMARY KEY (Title, PrintFormat);


--Rename a table?
ALTER TABLE Genres CHANGE COLUMN Genre TO Genre2;