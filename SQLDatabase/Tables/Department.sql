CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DepartmentName] VARCHAR(50) NOT NULL, 
    CONSTRAINT [AK_Department_DepartmentName] UNIQUE ([DepartmentName])
)
