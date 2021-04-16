CREATE TABLE [dbo].[Vacancy]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [JobTitle] VARCHAR(50) NOT NULL, 
    [JobDescription] NVARCHAR(MAX) NOT NULL, 
    [SalaryMin] INT NOT NULL, 
    [SalaryMax] INT NOT NULL, 
    [SalaryRangeId] INT NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    [ContractType] VARCHAR(50) NOT NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL, 
    [Published] BIT NULL DEFAULT 0, 
    CONSTRAINT [FK_Vacancy_SalaryRange] FOREIGN KEY ([SalaryRangeId]) REFERENCES [SalaryRange]([Id]), 
    CONSTRAINT [FK_Vacancy_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([Id]) 
)
