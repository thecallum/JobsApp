CREATE TABLE [dbo].[VacancyCustomQuestion]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VacancyId] INT NOT NULL, 
    [DisplayOrder] INT NOT NULL, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [IsRequired] BIT NOT NULL DEFAULT 0, 
    [MinLength] INT NOT NULL, 
    [MaxLength] INT NOT NULL, 
    CONSTRAINT [FK_VacancyCustomQuestion_Vacancy] FOREIGN KEY ([VacancyId]) REFERENCES [Vacancy]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [AK_VacancyCustomQuestion_VacancyId-DisplayOrder] UNIQUE ([VacancyId], [DisplayOrder])
)

