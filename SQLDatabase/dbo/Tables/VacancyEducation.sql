CREATE TABLE [dbo].[VacancyEducation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VacancyApplicationId] INT NOT NULL, 
    [EducationTypeId] INT NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_VacancyEducation_VacancyApplication] FOREIGN KEY ([VacancyApplicationId]) REFERENCES [VacancyApplication]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_VacancyEducation_EducationType] FOREIGN KEY ([EducationTypeId]) REFERENCES [EducationType]([Id])
)
