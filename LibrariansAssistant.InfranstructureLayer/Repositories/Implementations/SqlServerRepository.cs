using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using System.Text;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Implementations;

public sealed class SqlServerRepository : IRepository
{
    private SqlServerOrm? _sqlServerOrm;

    public void Initialize(string initializationString)
    {
        if (string.IsNullOrEmpty(initializationString) is true)
            throw new ArgumentNullException(nameof(initializationString),
                "Initialization string must not be null or empty.");

        try
        {
            _sqlServerOrm = new SqlServerOrm(initializationString);

            OrmDependenciesConfigurator.Configure(_sqlServerOrm);
        }
        catch
        {
            throw;
        }
    }

    public void AuthorAdd(IAuthorModel author)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("FirstName", author.FirstName)
                .AddParameter("LastName", author.LastName)
                .AddParameter("Patronymic", author.Patronymic);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "INSERT INTO authors (first_name, last_name, patronymic) " +
                "VALUES (@FirstName, @LastName, @Patronymic);",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IAuthorModel> AuthorGetAll()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IEnumerable<IAuthorModel> authors;

        try
        {
            authors = _sqlServerOrm
                .ExecuteQuery<IAuthorModel>(

                "SELECT * " +
                "FROM authors;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true
                }

                );
        }
        catch
        {
            throw;
        }

        return authors;
    }

    public IAuthorModel? AuthorGetById(int authorId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IAuthorModel? author;

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", authorId);

            author = _sqlServerOrm
                .ExecuteQuery<IAuthorModel>(

                "SELECT * " +
                "FROM authors " +
                "WHERE id = @Id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true
                }

                )
                .FirstOrDefault();
        }
        catch
        {
            throw;
        }

        return author;
    }

    public void AuthorUpdate(IAuthorModel author)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", author.Id)
                .AddParameter("FirstName", author.FirstName)
                .AddParameter("LastName", author.LastName)
                .AddParameter("Patronymic", author.Patronymic);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "UPDATE authors " +
                "SET first_name = @FirstName, " +
                "last_name = @LastName, " +
                "patronymic = @Patronymic " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void AuthorDelete(int authorId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", authorId);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "DELETE " +
                "FROM authors " +
                "WHERE id = @Id;",

                default

                );

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "DELETE " +
                "FROM books " +
                "WHERE id NOT IN " +
                "(" +
                "SELECT book_id " +
                "FROM authors_books" +
                ");",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void BookAdd(IBookModel book)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Title", book.Title)
                .AddParameter("Genre", book.Genre);

            IAuthorModel[] authors = book.Authors.ToArray();

            for (var i = 0; i < authors.Length; i++)
                _ = _sqlServerOrm.AddParameter("AuthorId" + (i + 1), authors[i].Id);

            StringBuilder query = new();

            _ = query.AppendLine("BEGIN TRY");
            _ = query.AppendLine("BEGIN TRANSACTION;");

            _ = query.AppendLine(

                "INSERT INTO books (title, genre) " +
                "VALUES (@Title, @Genre);"

                );

            _ = query.AppendLine("DECLARE @BookId INT = SCOPE_IDENTITY();");

            for (var i = 0; i < authors.Length; i++)
                _ = query.AppendLine(

                    "INSERT INTO authors_books (author_id, book_id) " +
                    $"VALUES (@AuthorId{i + 1}, @BookId);"

                    );

            _ = query.AppendLine("IF @@TRANCOUNT > 0 COMMIT TRANSACTION;");
            _ = query.AppendLine("END TRY");

            _ = query.AppendLine("BEGIN CATCH");
            _ = query.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;");
            _ = query.AppendLine("THROW;");
            _ = query.Append("END CATCH;");

            _ = _sqlServerOrm.ExecuteNonQuery(query.ToString(), default);
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IBookModel> BookGetAll()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IEnumerable<IBookModel> books;

        try
        {
            books = _sqlServerOrm
                .ExecuteQuery<IBookModel>(

                "SELECT id AS book_id, title AS book_title, genre AS book_genre " +
                "FROM books;" +

                Environment.NewLine + Environment.NewLine +

                "SELECT authors.id AS author_id, authors.first_name AS author_first_name, " +
                "authors.last_name AS author_last_name, authors.patronymic AS author_patronymic, " +
                "(ROW_NUMBER() OVER(PARTITION BY books.id ORDER BY books.id) - 1) AS i " +
                "FROM authors " +
                "INNER JOIN authors_books ON authors.id = authors_books.author_id " +
                "INNER JOIN books ON authors_books.book_id = books.id " +
                "ORDER BY books.id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true,
                    AddPropertyTypeObjectNamePrefix = true,
                    SuppressModelInPrefix = true
                }

                );
        }
        catch
        {
            throw;
        }

        return books;
    }

    public IBookModel? BookGetById(int bookId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IBookModel? book;

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", bookId);

            book = _sqlServerOrm
                .ExecuteQuery<IBookModel>(

                "SELECT id AS book_id, title AS book_title, genre AS book_genre " +
                "FROM books " +
                "WHERE id = @Id;" +

                Environment.NewLine + Environment.NewLine +

                "SELECT authors.id AS author_id, authors.first_name AS author_first_name, " +
                "authors.last_name AS author_last_name, authors.patronymic AS author_patronymic, " +
                "(ROW_NUMBER() OVER(PARTITION BY books.id ORDER BY books.id) - 1) AS i " +
                "FROM authors " +
                "INNER JOIN authors_books ON authors.id = authors_books.author_id " +
                "INNER JOIN books ON authors_books.book_id = books.id " +
                "WHERE books.id = @Id " +
                "ORDER BY books.id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true,
                    AddPropertyTypeObjectNamePrefix = true,
                    SuppressModelInPrefix = true
                }

                )
                .FirstOrDefault();
        }
        catch
        {
            throw;
        }

        return book;
    }

    public void BookUpdate(IBookModel book)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("BookId", book.Id)
                .AddParameter("Title", book.Title)
                .AddParameter("Genre", book.Genre);

            IAuthorModel[] authors = book.Authors.ToArray();

            for (var i = 0; i < authors.Length; i++)
                _ = _sqlServerOrm.AddParameter("AuthorId" + (i + 1), authors[i].Id);

            StringBuilder query = new();

            _ = query.AppendLine("BEGIN TRY");
            _ = query.AppendLine("BEGIN TRANSACTION;");

            _ = query.AppendLine(

                "UPDATE books " +
                "SET title = @Title, " +
                "genre = @Genre " +
                "WHERE id = @BookId;"

                );

            _ = query.AppendLine(

                "DELETE " +
                "FROM authors_books " +
                "WHERE book_id = @BookId;"

                );

            for (var i = 0; i < authors.Length; i++)
                _ = query.AppendLine(

                    "INSERT INTO authors_books (author_id, book_id) " +
                    $"VALUES (@AuthorId{i + 1}, @BookId);"

                    );

            _ = query.AppendLine("IF @@TRANCOUNT > 0 COMMIT TRANSACTION;");
            _ = query.AppendLine("END TRY");

            _ = query.AppendLine("BEGIN CATCH");
            _ = query.AppendLine("IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;");
            _ = query.AppendLine("THROW;");
            _ = query.Append("END CATCH;");

            _ = _sqlServerOrm.ExecuteNonQuery(query.ToString(), default);
        }
        catch
        {
            throw;
        }
    }

    public void BookDelete(int bookId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", bookId);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "DELETE " +
                "FROM books " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void IssuingAdd(IIssuingModel issuing)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("ReaderId", issuing.Reader.Id)
                .AddParameter("BookId", issuing.Book.Id)
                .AddParameter("TakeDate", issuing.TakeDate)
                .AddParameter("Returned", issuing.Returned)
                .AddParameter("ReturnDate", issuing.ReturnDate)
                .AddParameter("ReturnState", issuing.ReturnState);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "INSERT INTO issuings (reader_id, book_id, take_date, returned, return_date, return_state) " +
                "VALUES (@ReaderId, @BookId, @TakeDate, @Returned, @ReturnDate, @ReturnState);",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IIssuingModel> IssuingGetAll()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IEnumerable<IIssuingModel> issuings;

        try
        {
            issuings = _sqlServerOrm
                .ExecuteQuery<IIssuingModel>(

                "SELECT issuings.id AS issuing_id, readers.id AS reader_id, readers.first_name AS reader_first_name, " +
                "readers.last_name AS reader_last_name, readers.patronymic AS reader_patronymic, " +
                "readers.gender AS reader_gender, readers.date_of_birth AS reader_date_of_birth, " +
                "books.id AS book_id, books.title AS book_title, books.genre AS book_genre, " +
                "issuings.take_date AS issuing_take_date, issuings.returned AS issuing_returned, " +
                "issuings.return_date AS issuing_return_date, issuings.return_state AS issuing_return_state " +
                "FROM issuings " +
                "INNER JOIN readers ON issuings.reader_id = readers.id " +
                "INNER JOIN books ON issuings.book_id = books.id;" +

                Environment.NewLine + Environment.NewLine +

                "SELECT authors.id AS author_id, authors.first_name AS author_first_name, " +
                "authors.last_name AS author_last_name, authors.patronymic AS author_patronymic, " +
                "(ROW_NUMBER() OVER(PARTITION BY issuings.id ORDER BY issuings.id) - 1) AS i " +
                "FROM authors " +
                "INNER JOIN authors_books ON authors.id = authors_books.author_id " +
                "INNER JOIN books ON authors_books.book_id = books.id " +
                "INNER JOIN issuings ON authors_books.book_id = issuings.book_id " +
                "ORDER BY issuings.id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true,
                    AddPropertyTypeObjectNamePrefix = true,
                    SuppressModelInPrefix = true
                }

                );
        }
        catch
        {
            throw;
        }

        return issuings;
    }

    public IIssuingModel? IssuingGetById(int issuingId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IIssuingModel? issuing;

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", issuingId);

            issuing = _sqlServerOrm
                .ExecuteQuery<IIssuingModel>(

                "SELECT issuings.id AS issuing_id, readers.id AS reader_id, readers.first_name AS reader_first_name, " +
                "readers.last_name AS reader_last_name, readers.patronymic AS reader_patronymic, " +
                "readers.gender AS reader_gender, readers.date_of_birth AS reader_date_of_birth, " +
                "books.id AS book_id, books.title AS book_title, books.genre AS book_genre, " +
                "issuings.take_date AS issuing_take_date, issuings.returned AS issuing_returned, " +
                "issuings.return_date AS issuing_return_date, issuings.return_state AS issuing_return_state " +
                "FROM issuings " +
                "INNER JOIN readers ON issuings.reader_id = readers.id " +
                "INNER JOIN books ON issuings.book_id = books.id " +
                "WHERE issuings.id = @Id;" +

                Environment.NewLine + Environment.NewLine +

                "SELECT authors.id AS author_id, authors.first_name AS author_first_name, " +
                "authors.last_name AS author_last_name, authors.patronymic AS author_patronymic, " +
                "(ROW_NUMBER() OVER(PARTITION BY issuings.id ORDER BY issuings.id) - 1) AS i " +
                "FROM authors " +
                "INNER JOIN authors_books ON authors.id = authors_books.author_id " +
                "INNER JOIN books ON authors_books.book_id = books.id " +
                "INNER JOIN issuings ON authors_books.book_id = issuings.book_id " +
                "WHERE issuings.id = @Id " +
                "ORDER BY issuings.id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true,
                    AddPropertyTypeObjectNamePrefix = true,
                    SuppressModelInPrefix = true
                }

                )
                .FirstOrDefault();
        }
        catch
        {
            throw;
        }

        return issuing;
    }

    public void IssuingUpdate(IIssuingModel issuing)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", issuing.Id)
                .AddParameter("ReaderId", issuing.Reader.Id)
                .AddParameter("BookId", issuing.Book.Id)
                .AddParameter("TakeDate", issuing.TakeDate)
                .AddParameter("Returned", issuing.Returned)
                .AddParameter("ReturnDate", issuing.ReturnDate)
                .AddParameter("ReturnState", issuing.ReturnState);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "UPDATE issuings " +
                "SET reader_id = @ReaderId, " +
                "book_id = @BookId, " +
                "take_date = @TakeDate, " +
                "returned = @Returned, " +
                "return_date = @ReturnDate, " +
                "return_state = @ReturnState " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void IssuingDelete(int issuingId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", issuingId);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "DELETE " +
                "FROM issuings " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void ReaderAdd(IReaderModel reader)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("FirstName", reader.FirstName)
                .AddParameter("LastName", reader.LastName)
                .AddParameter("Patronymic", reader.Patronymic)
                .AddParameter("Gender", reader.Gender)
                .AddParameter("DateOfBirth", reader.DateOfBirth);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "INSERT INTO readers (first_name, last_name, patronymic, gender, date_of_birth) " +
                "VALUES (@FirstName, @LastName, @Patronymic, @Gender, @DateOfBirth);",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IReaderModel> ReaderGetAll()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IEnumerable<IReaderModel> readers;

        try
        {
            readers = _sqlServerOrm
                .ExecuteQuery<IReaderModel>(

                "SELECT * " +
                "FROM readers;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true
                }

                );
        }
        catch
        {
            throw;
        }

        return readers;
    }

    public IReaderModel? ReaderGetById(int readerId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        IReaderModel? reader;

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", readerId);

            reader = _sqlServerOrm
                .ExecuteQuery<IReaderModel>(

                "SELECT * " +
                "FROM readers " +
                "WHERE id = @Id;",

                default,
                new MappingSettings()
                {
                    UseSqlStylePropertiesNaming = true
                }

                )
                .FirstOrDefault();
        }
        catch
        {
            throw;
        }

        return reader;
    }

    public void ReaderUpdate(IReaderModel reader)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", reader.Id)
                .AddParameter("FirstName", reader.FirstName)
                .AddParameter("LastName", reader.LastName)
                .AddParameter("Patronymic", reader.Patronymic)
                .AddParameter("Gender", reader.Gender)
                .AddParameter("DateOfBirth", reader.DateOfBirth);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "UPDATE readers " +
                "SET first_name = @FirstName, " +
                "last_name = @LastName, " +
                "patronymic = @Patronymic, " +
                "gender = @Gender, " +
                "date_of_birth = @DateOfBirth " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }

    public void ReaderDelete(int readerId)
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because the repository has not been initialized.");

        try
        {
            _ = _sqlServerOrm
                .AddParameter("Id", readerId);

            _ = _sqlServerOrm
                .ExecuteNonQuery(

                "DELETE " +
                "FROM readers " +
                "WHERE id = @Id;",

                default

                );
        }
        catch
        {
            throw;
        }
    }
}