CREATE TABLE [dbo].[issuings]
(
    [id] INT NOT NULL IDENTITY,
    [reader_id] INT NOT NULL,
    [book_id] INT NOT NULL,
    [take_date] DATE NOT NULL,
    [returned] BIT NOT NULL,
    [return_date] DATE NULL,
    [return_state] INT NULL,
    CONSTRAINT [PK_issuings] PRIMARY KEY ([id]),
    CONSTRAINT [FK_issuings_readers] FOREIGN KEY ([reader_id]) REFERENCES [readers]([id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_issuings_books] FOREIGN KEY ([book_id]) REFERENCES [books]([id]) ON DELETE CASCADE ON UPDATE CASCADE
)