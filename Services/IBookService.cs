using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IBookService
{
    public List<BookEditionDTO> Books(int BookId);
    public int AddBook(AddBookDTO addBookDto);


}