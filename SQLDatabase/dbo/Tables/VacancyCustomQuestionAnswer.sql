CREATE TABLE [dbo].[VacancyCustomQuestionAnswer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VacancyApplicationId] INT NOT NULL, 
    [VacancyCustomQuestionId] INT NOT NULL, 
    [Answer] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_VacancyCustomQuestionAnswer_VacancyApplication] FOREIGN KEY ([VacancyApplicationId]) REFERENCES [VacancyApplication]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_VacancyCustomQuestionAnswer_VacancyCustomQuestion] FOREIGN KEY ([VacancyCustomQuestionId]) REFERENCES [VacancyCustomQuestion]([Id]) ON DELETE CASCADE
)
