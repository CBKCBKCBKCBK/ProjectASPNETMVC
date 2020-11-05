﻿CREATE TABLE [dbo].[GuestBook]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(100) NOT NULL, 
    [Content] NVARCHAR(500) NOT NULL, 
    [PostTime] DATETIME NOT NULL
)
