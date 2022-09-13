CREATE TABLE [dbo].[authors_books]
(
	[author_id] INT NOT NULL, 
    [book_id] INT NOT NULL, 
    CONSTRAINT [PK_authors_books] PRIMARY KEY ([author_id], [book_id]), 
    CONSTRAINT [FK_authors_books_authors] FOREIGN KEY ([author_id]) REFERENCES [authors]([id]) ON DELETE CASCADE ON UPDATE CASCADE, 
    CONSTRAINT [FK_authors_books_books] FOREIGN KEY ([book_id]) REFERENCES [books]([id]) ON DELETE CASCADE ON UPDATE CASCADE
)
