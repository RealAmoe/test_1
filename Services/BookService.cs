using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class BookService : IBookService
{
    private IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<BookEditionDTO> Books(int BookId)
    {
        return _bookRepository.Books(BookId);
    }

    public int AddBook(AddBookDTO addBookDto)
    {
        return _bookRepository.AddBook(addBookDto);
    }
}