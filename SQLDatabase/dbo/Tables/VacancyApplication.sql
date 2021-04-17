CREATE TABLE [dbo].[VacancyApplication]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VacancyId] INT NOT NULL, 
    [AddressLine1] VARCHAR(50) NOT NULL, 
    [AddressLine2] VARCHAR(50) NULL, 
    [AddressLine3] VARCHAR(50) NULL, 
    [AddressLine4] VARCHAR(50) NULL, 
    [PostCode] VARCHAR(10) NOT NULL, 
    [PhoneNumber] VARCHAR(20) NOT NULL, 
    [EmailAddress] NVARCHAR(100) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_VacancyApplication_Vacancy] FOREIGN KEY ([VacancyId]) REFERENCES [Vacancy]([Id]) ON DELETE CASCADE
)
