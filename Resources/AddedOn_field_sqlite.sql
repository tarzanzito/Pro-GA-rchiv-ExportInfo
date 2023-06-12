
=======================================
PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table AS SELECT *
                                          FROM Artists;

DROP TABLE Artists;

CREATE TABLE Artists (
    ID         INTEGER PRIMARY KEY
                       NOT NULL,
    Artist     TEXT,
    Country_ID INTEGER NOT NULL
                       DEFAULT (0),
    Country    TEXT,
    Style      TEXT,
    Inactive   BOOLEAN DEFAULT (0),
    AddedOn    TEXT
);

INSERT INTO Artists (
                        ID,
                        Artist,
                        Country_ID,
                        Country,
                        Style,
                        Inactive
                    )
                    SELECT ID,
                           Artist,
                           Country_ID,
                           Country,
                           Style,
                           Inactive
                      FROM sqlitestudio_temp_table;

DROP TABLE sqlitestudio_temp_table;

CREATE UNIQUE INDEX Artists_Artist_Index ON Artists (
    Artist,
    ID
);

PRAGMA foreign_keys = 1;
=======================================
PRAGMA foreign_keys = 0;

CREATE TABLE sqlitestudio_temp_table AS SELECT *
                                          FROM Albums;

DROP TABLE Albums;

CREATE TABLE Albums (
    ID          INTEGER NOT NULL,
    Album       TEXT,
    Artist_ID   INTEGER NOT NULL
                        DEFAULT (0),
    Artist      TEXT,
    Cover       TEXT,
    YearAndType TEXT,
    Tracks      TEXT,
    Musicians   TEXT,
    YearN       TEXT,
    Type        TEXT,
    Inactive    BOOLEAN DEFAULT (0),
    AddedOn     TEXT,
    PRIMARY KEY (
        ID
    )
);

INSERT INTO Albums (
                       ID,
                       Album,
                       Artist_ID,
                       Artist,
                       Cover,
                       YearAndType,
                       Tracks,
                       Musicians,
                       YearN,
                       Type,
                       Inactive
                   )
                   SELECT ID,
                          Album,
                          Artist_ID,
                          Artist,
                          Cover,
                          YearAndType,
                          Tracks,
                          Musicians,
                          YearN,
                          Type,
                          Inactive
                     FROM sqlitestudio_temp_table;

DROP TABLE sqlitestudio_temp_table;

PRAGMA foreign_keys = 1;
