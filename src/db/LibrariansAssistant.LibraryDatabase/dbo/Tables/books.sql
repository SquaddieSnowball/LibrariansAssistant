CREATE TABLE [dbo].[books]
(
    [id] INT NOT NULL IDENTITY,
    [title] NVARCHAR(50) NOT NULL,
    [genre] NVARCHAR(25) NOT NULL,
    CONSTRAINT [PK_books] PRIMARY KEY ([id])
)