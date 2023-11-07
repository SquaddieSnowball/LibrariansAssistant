IF NOT EXISTS
    (
        SELECT *
        FROM sys.databases
        WHERE [name] = 'library'
    )
    BEGIN
        CREATE DATABASE [library];
    END;
GO

USE [library];
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sysobjects
        WHERE [name] = 'readers' AND [type] = 'U'
    )
    BEGIN
        CREATE TABLE [dbo].[readers]
        (
            [id]            INT           IDENTITY (1, 1) NOT NULL,
            [first_name]    NVARCHAR (25) NOT NULL,
            [last_name]     NVARCHAR (50) NOT NULL,
            [patronymic]    NVARCHAR (25) NULL,
            [gender]        NVARCHAR (25) NOT NULL,
            [date_of_birth] DATE          NOT NULL,
            CONSTRAINT [PK_readers] PRIMARY KEY CLUSTERED ([id] ASC)
        );
    END;
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sysobjects
        WHERE [name] = 'authors' AND [type] = 'U'
    )
    BEGIN
        CREATE TABLE [dbo].[authors]
        (
            [id]         INT           IDENTITY (1, 1) NOT NULL,
            [first_name] NVARCHAR (25) NOT NULL,
            [last_name]  NVARCHAR (50) NOT NULL,
            [patronymic] NVARCHAR (25) NULL,
            CONSTRAINT [PK_authors] PRIMARY KEY CLUSTERED ([id] ASC)
        );
    END;
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sysobjects
        WHERE [name] = 'books' AND [type] = 'U'
    )
    BEGIN
        CREATE TABLE [dbo].[books]
        (
            [id]    INT           IDENTITY (1, 1) NOT NULL,
            [title] NVARCHAR (50) NOT NULL,
            [genre] NVARCHAR (25) NOT NULL,
            CONSTRAINT [PK_books] PRIMARY KEY CLUSTERED ([id] ASC)
        );
    END;
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sysobjects
        WHERE [name] = 'authors_books' AND [type] = 'U'
    )
    BEGIN
        CREATE TABLE [dbo].[authors_books]
        (
            [author_id] INT NOT NULL,
            [book_id]   INT NOT NULL,
            CONSTRAINT [PK_authors_books] PRIMARY KEY CLUSTERED ([author_id] ASC, [book_id] ASC)
        );
        
        ALTER TABLE [dbo].[authors_books]
            ADD CONSTRAINT [FK_authors_books_authors] FOREIGN KEY ([author_id]) REFERENCES [dbo].[authors] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

        ALTER TABLE [dbo].[authors_books]
            ADD CONSTRAINT [FK_authors_books_books] FOREIGN KEY ([book_id]) REFERENCES [dbo].[books] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;
    END;
GO

IF NOT EXISTS
    (
        SELECT *
        FROM sysobjects
        WHERE [name] = 'issuings' AND [type] = 'U'
    )
    BEGIN
        CREATE TABLE [dbo].[issuings]
        (
            [id]           INT  IDENTITY (1, 1) NOT NULL,
            [reader_id]    INT  NOT NULL,
            [book_id]      INT  NOT NULL,
            [take_date]    DATE NOT NULL,
            [returned]     BIT  NOT NULL,
            [return_date]  DATE NULL,
            [return_state] INT  NULL,
            CONSTRAINT [PK_issuings] PRIMARY KEY CLUSTERED ([id] ASC)
        );
        
        ALTER TABLE [dbo].[issuings]
            ADD CONSTRAINT [FK_issuings_readers] FOREIGN KEY ([reader_id]) REFERENCES [dbo].[readers] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;

        ALTER TABLE [dbo].[issuings]
            ADD CONSTRAINT [FK_issuings_books] FOREIGN KEY ([book_id]) REFERENCES [dbo].[books] ([id]) ON DELETE CASCADE ON UPDATE CASCADE;
    END;
GO