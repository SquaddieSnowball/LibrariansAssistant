CREATE TABLE [dbo].[readers]
(
	[id] INT NOT NULL IDENTITY, 
    [first_name] NVARCHAR(25) NOT NULL, 
    [last_name] NVARCHAR(50) NOT NULL, 
    [patronymic] NVARCHAR(25) NULL, 
    [gender] NVARCHAR(25) NOT NULL, 
    [date_of_birth] DATE NOT NULL, 
    CONSTRAINT [PK_readers] PRIMARY KEY ([id]) 
)
