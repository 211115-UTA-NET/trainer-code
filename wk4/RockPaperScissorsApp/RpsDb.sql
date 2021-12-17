--DROP TABLE Rps.Round;
--DROP TABLE Rps.Move;
--DROP TABLE Rps.Player;
--DROP SCHEMA Rps;
--GO

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
    
CREATE NONCLUSTERED INDEX IX_Round_Player1
    ON Rps.Round (Player1);
CREATE NONCLUSTERED INDEX IX_Round_Player2
    ON Rps.Round (Player2);
CREATE NONCLUSTERED INDEX IX_Round_Player1Move
    ON Rps.Round (Player1Move);
CREATE NONCLUSTERED INDEX IX_Round_Player2Move
    ON Rps.Round (Player2Move);

INSERT INTO Rps.Move (Name) VALUES
    ('Rock'), ('Paper'), ('Scissors');

INSERT INTO Rps.Player (Name) VALUES
    ('Nick');

INSERT INTO Rps.Round (Player1, Player2, Player1Move, Player2Move) VALUES
    ((SELECT Id FROM Rps.Player WHERE Name = 'Nick'), NULL,
        (SELECT Id FROM Rps.Move WHERE Name = 'Paper'), (SELECT Id FROM Rps.Move WHERE Name = 'Rock'));

-- workspace

SELECT Timestamp, P1.Name, P2.Name, P1M.Name, P2M.Name
                  FROM Rps.Round
                      INNER JOIN Rps.Player AS P1 ON Player1 = P1.Id
                      LEFT JOIN Rps.Player AS P2 ON Player2 = P2.Id
                      INNER JOIN Rps.Move AS P1M ON Player1Move = P1M.Id
                      INNER JOIN Rps.Move AS P2M ON Player2Move = P2M.Id
                  WHERE P1.Name = 'Nick';
                  
SELECT * FROM Rps.Round;
SELECT * FROM Rps.Player;