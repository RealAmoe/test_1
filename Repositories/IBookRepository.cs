using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IBookRepository
{
    public List<BookEditionDTO> Books(int BookId);
    public int AddBook(AddBookDTO addBookDto);
}