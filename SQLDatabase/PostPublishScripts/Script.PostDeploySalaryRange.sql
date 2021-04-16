/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


if not exists (select 1 from dbo.SalaryRange)
begin
    insert into dbo.SalaryRange (MinAmount, MaxAmount)
    values (0, 20000), (20001, 40000), (40001, 60000), (60001, 100000);
end