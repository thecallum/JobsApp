CREATE TABLE [dbo].[EducationType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(20) NOT NULL, 
    CONSTRAINT [AK_EducationType_Name] UNIQUE ([Name])
)
