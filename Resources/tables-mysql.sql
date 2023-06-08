
---------
- MySql - todo
---------

CREATE DATABASE "ProgArchives"

DROP TABLE "Countries";

CREATE TABLE "Countries" (
	"ID" INTEGER NOT NULL,
	"Country" TEXT NULL,
	"Inactive" BOOLEAN DEFAULT (0),
	PRIMARY KEY ("ID"),
	UNIQUE INDEX "Countries_Country_Index" ("Country", "ID")
)



DROP TABLE "Artists";

CREATE TABLE "Artists" (
	"ID" INTEGER NOT NULL,
	"Artist" TEXT NULL,
	"Country_ID" INTEGER NULL,
	"Country" TEXT NULL,
	"Style" TEXT NULL,
	"Inactive"  BOOLEAN DEFAULT (0),
	"AddedOn" DATE,
	PRIMARY KEY ("ID"),
	UNIQUE INDEX "Artists_Artist_Index" ("Artist", "ID")
)
;



DROP TABLE "Albums";

CREATE TABLE "Albums" (
	"ID" INTEGER NOT NULL,
	"Album" TEXT NULL,
	"Artist_ID" INTEGER NULL,
	"Artist" TEXT NULL,
	"Cover" TEXT NULL,
	"YearAndType" TEXT NULL,
	"Tracks" TEXT NULL,
	"Musicians" TEXT NULL,
	"YearN" TEXT NULL,
	"Type" TEXT NULL,
	"Inactive"  BOOLEAN DEFAULT (0),
	"AddedOn" DATE,
	PRIMARY KEY ("ID"),
	UNIQUE INDEX "Albums_Album_Index" ("Album", "ID")
)
;