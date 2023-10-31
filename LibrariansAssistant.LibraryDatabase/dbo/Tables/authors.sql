CREATE TABLE [dbo].[authors]
(
    [id] INT NOT NULL IDENTITY,
    [first_name] NVARCHAR(25) NOT NULL,
    [last_name] NVARCHAR(50) NOT NULL,
    [patronymic] NVARCHAR(25) NULL,
    CONSTRAINT [PK_authors] PRIMARY KEY ([id])
)