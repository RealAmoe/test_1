using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{BookId}/editions")]
    public IActionResult getBooks(int BookId)
    {
        var AllBooks = _bookService.Books(BookId);
        if (AllBooks == null)
        {
            return BadRequest("An error occured");
        }

        return Ok(AllBooks);
    }

    [HttpPost]
    public IActionResult AddBook(AddBookDTO addBookDto)
    {
        var res = _bookService.AddBook(addBookDto);
        if (res == -1)
            return NotFound("Publishing House not found");
        if (res == -2)
            return BadRequest("Error while adding Book in book table");
        if (res == 0)
            return BadRequest("An error occured");

        return Created(Request.Path.Value ?? "api/books", addBookDto);
    }
}