using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class BookRepository : IBookRepository
{
    private IConfiguration _configuration;

    public BookRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    //SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:Default"];
    public List<BookEditionDTO> Books(int BookId)
    {
        var books = new List<BookEditionDTO>();
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = @"SELECT books_editions.PK as Id, books.title as BookTitle, books_editions.edition_title as BEditionTitle,
                                publishing_houses.name as PHName, books_editions.release_date as Date
                                FROM books
                                JOIN books_editions on books_editions.FK_book = books.PK
                                JOIN publishing_houses on publishing_houses.PK = books_editions.FK_publishing_house
                                WHERE books.PK = @BookId";
        command.Parameters.AddWithValue("@BookId", BookId);
        
        using SqlDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
            return null;
        
        while (reader.Read())
        {
            books.Add(new BookEditionDTO()
            {
                Id = (int)reader["Id"], BookTitle = (string)reader["BookTitle"], EditionTitle = (string)reader["BEditionTitle"], PublishingHouseName = (string)reader["PHName"], ReleaseDate = (DateTime)reader["Date"]
            });
        }

        return books;
    }

    public int AddBook(AddBookDTO addBookDto)
    {
        using SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:Default"]);
        connection.Open();
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "Select 1 FROM publishing_houses where PK = @Id";
        command.Parameters.AddWithValue("@Id", addBookDto.PublishingHouseId);
        
        var check = command.ExecuteScalar();
        if (check != null)
        {
            command.Parameters.Clear();
            command.CommandText = @"INSERT INTO books
                                    VALUES (@Title); Select Scope_Identity()";
            command.Parameters.AddWithValue("@Title", addBookDto.BookTitle);

            var check1 = command.ExecuteScalar();
            if (check1 != null)
            {
                command.Parameters.Clear();
                command.CommandText = @"INSERT INTO books_editions
                VALUES (@PHId, @BId, @EditionName, @Date); SELECT Scope_Identity()";
                command.Parameters.AddWithValue("@PHId", addBookDto.PublishingHouseId);
                command.Parameters.AddWithValue("@BId", Convert.ToInt32(check1));
                command.Parameters.AddWithValue("@EditionName", addBookDto.EditionTitle);
                command.Parameters.AddWithValue("@Date", addBookDto.ReleaseDate);
                
                var check2 = command.ExecuteScalar();
                if (check != null)
                {
                    return Convert.ToInt32(check2);
                }
            }
            else
            {
                return -2;
            }
        }
        else
        {
            return -1;
        }
        return 0;
    }
}