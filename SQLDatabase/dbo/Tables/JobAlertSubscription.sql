CREATE TABLE [dbo].[JobAlertSubscription]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [EmailAddress] NVARCHAR(100) NOT NULL, 
    [SalaryRangeId] INT NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    CONSTRAINT [FK_JobAlertSubscription_SalaryRange] FOREIGN KEY ([SalaryRangeId]) REFERENCES [SalaryRange]([Id]), 
    CONSTRAINT [FK_JobAlertSubscription_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [Department]([Id]), 
    CONSTRAINT [CK_JobAlertSubscription_EmailAddress] UNIQUE (EmailAddress) 
)
