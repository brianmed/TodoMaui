# TodoMaui

What everyone needs!  A todo app with .Net MAUI, Entity Framework Core, SQLite, and DependencyInjection.

Tested in iOS and macOS.

## macOS

```bash
$ dotnet build -t:Run -f net6.0-maccatalyst
...
$ sqlite3 /Users/bpm/Documents/TodoMaui/AppDb/TodoMaui/TodoMaui.sqlite
SQLite version 3.32.3 2020-06-18 14:16:19
Enter ".help" for usage hints.
sqlite> .dump
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Todo" (
    "TodoId" INTEGER NOT NULL CONSTRAINT "PK_Todo" PRIMARY KEY AUTOINCREMENT,
    "Note" TEXT NULL,
    "IsDone" INTEGER NOT NULL,
    "Updated" TEXT NOT NULL,
    "Inserted" TEXT NOT NULL
);
INSERT INTO Todo VALUES(1,'Buy Something',0,'0001-01-01 00:00:00','2022-03-24 01:55:27.168887');
INSERT INTO Todo VALUES(2,'Sell Something',0,'0001-01-01 00:00:00','2022-03-24 01:55:35.282077');
INSERT INTO Todo VALUES(3,'Have Fun',1,'2022-03-24 01:55:47.553716','2022-03-24 01:55:42.400382');
INSERT INTO Todo VALUES(4,'',0,'0001-01-01 00:00:00','2022-03-24 01:56:19.907967');
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Todo',4);
COMMIT;
sqlite> 
```

![alt text](http://bmedley.org/adhoc/2022-03-23/todoMaui_macOS.png)

## iOS

```bash
$ sqlite3 .../Containers/Data/Application/555C8231-678A-42D8-BB69-D528AE35E63A/Documents/TodoMaui/AppDb/TodoMaui/TodoMaui.sqlite

SQLite version 3.32.3 2020-06-18 14:16:19
Enter ".help" for usage hints.
sqlite> .dump
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Todo" (
    "TodoId" INTEGER NOT NULL CONSTRAINT "PK_Todo" PRIMARY KEY AUTOINCREMENT,
    "Note" TEXT NULL,
    "IsDone" INTEGER NOT NULL,
    "Updated" TEXT NOT NULL,
    "Inserted" TEXT NOT NULL
);
INSERT INTO Todo VALUES(1,'Drive around',1,'2022-03-24 01:57:41.984683','2022-03-24 01:57:30.763776');
INSERT INTO Todo VALUES(2,'Have Fun',0,'0001-01-01 00:00:00','2022-03-24 01:57:38.288723');
DELETE FROM sqlite_sequence;
INSERT INTO sqlite_sequence VALUES('Todo',2);
COMMIT;
```

![alt text](http://bmedley.org/adhoc/2022-03-23/todoMaui_iOS.png)
