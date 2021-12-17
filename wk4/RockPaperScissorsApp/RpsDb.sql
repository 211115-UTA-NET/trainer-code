DROP TABLE Rps.Round;
DROP TABLE Rps.Move;
DROP TABLE Rps.Player;
DROP SCHEMA Rps;
GO

CREATE SCHEMA Rps;
GO

-- even if we have a nice candidate key that's human-friendly
--   we often don't want to pick that as our primary key, because
--   it's awkward to change a primary key value and it's nice to 
--   leave ourselves freedom to change that nice candidate key value

-- options for inventing a new primary key for a given table:
--   - autoincrementing int
--   - GUID
--   - hi-lo sequence

CREATE TABLE Rps.Player (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Rps.Round (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    Timestamp DATETIMEOFFSET NOT NULL DEFAULT (SYSDATETIMEOFFSET()),
    Player1 INT NULL,
    Player2 INT NULL,
    Player1Move INT NOT NULL,
    Player2Move INT NOT NULL,
);

CREATE TABLE Rps.Move (
    Id INT NOT NULL IDENTITY PRIMARY KEY,
    Name NVARCHAR(30) NOT NULL UNIQUE
);

ALTER TABLE Rps.Round ADD CONSTRAINT FK_Round_Player1
    FOREIGN KEY (Player1) REFERENCES Rps.Player (Id);
ALTER TABLE Rps.Round ADD CONSTRAINT FK_Round_Player2
    FOREIGN KEY (Player2) REFERENCES Rps.Player (Id);
ALTER TABLE Rps.Round ADD CONSTRAINT FK_Round_Player1Move
    FOREIGN KEY (Player1Move) REFERENCES Rps.Move (Id);
ALTER TABLE Rps.Round ADD CONSTRAINT FK_Round_Player2Move
    FOREIGN KEY (Player2Move) REFERENCES Rps.Move (Id);

INSERT INTO Rps.Move (Name) VALUES
    ('Rock'), ('Paper'), ('Scissors');