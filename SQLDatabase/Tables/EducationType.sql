﻿CREATE TABLE [dbo].[EducationType]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(20) NOT NULL, 
    CONSTRAINT [AK_EducationType_Name] UNIQUE ([Name])
)
