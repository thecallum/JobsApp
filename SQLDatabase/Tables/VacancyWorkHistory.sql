CREATE TABLE [dbo].[VacancyWorkHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VacancyApplicationId] INT NOT NULL, 
    [EmployerName] VARCHAR(50) NOT NULL, 
    [JobTitle] VARCHAR(50) NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NULL, 
    [Summary] NVARCHAR(MAX) NOT NULL, 
    [DisplayOrder] INT NOT NULL, 
    CONSTRAINT [FK_VacancyWorkHistory_VacancyApplication] FOREIGN KEY ([VacancyApplicationId]) REFERENCES [VacancyApplication]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [AK_VacancyWorkHistory_DisplayOrder-VacancyApplicationId] UNIQUE ([DisplayOrder], [VacancyApplicationId])
)
