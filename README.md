# Librarians Assistant

![CI](https://github.com/SquaddieSnowball/LibrariansAssistant/actions/workflows/ci.yml/badge.svg)

**Librarians Assistant** is a simple assistant in library management. Key features:

- Accounting for borrowed books;
- Sort and filter existing records;
- Export data to various formats.

## How to use

### Requirements

- An instance of **Microsoft SQL Server** installed on the host machine;
- Available database name `library` on the DBMS instance, which will be used when running the application.

### Setup steps

- Configure connection settings in the `Application → Settings → Connection` section;
- Check the `If the database is not found, create a new empty one` checkbox in the `Application → Settings → Additional` section so that the database is created automatically if it is missing;
- Go to `Application → Connect to database` to display data.

## License

**Librarians Assistant** is licensed under the [MIT license](LICENSE.txt).
