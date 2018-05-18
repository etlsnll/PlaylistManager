
-- At the moment EF core doesn't allow us to create code that compiles into SQL queries that have CONTAINS or FREETEXT
-- statements that make use of these full text indexes, but we will create them for future use here anyhow:

USE [MusicLibrary]

CREATE FULLTEXT CATALOG FullTextCatalog AS DEFAULT;
GO

CREATE FULLTEXT INDEX ON  dbo.Tracks (Title) KEY INDEX PK_Tracks
WITH 
    CHANGE_TRACKING = AUTO, 
    STOPLIST=OFF
;
GO

CREATE FULLTEXT INDEX ON  dbo.Albums (Title) KEY INDEX PK_Albums
WITH 
    CHANGE_TRACKING = AUTO, 
    STOPLIST=OFF
;
GO

CREATE FULLTEXT INDEX ON  dbo.Artists (Title) KEY INDEX PK_Artists
WITH 
    CHANGE_TRACKING = AUTO, 
    STOPLIST=OFF
;
GO
