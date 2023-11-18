using Microsoft.AspNetCore.Mvc;
using webApi.Services;
using webApi.DataClasses.Entities;
using FluentValidation.Results;
using FluentValidation;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BooksController : ControllerBase
{
    private IBooksService _service;
    private IValidator<BookCl> _clValidator;
    private IValidator<Book> _validator;

    public BooksController(IBooksService service, IValidator<BookCl> clValidator, IValidator<Book> validator)
    {
        _service = service;
        _clValidator = clValidator;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(BookCl bookCl)
    {
        ValidationResult validResult = _clValidator.Validate(bookCl);
        if (validResult.IsValid)
        {
            var result = await _service.AddBook(bookCl);

            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
        else
        {
            return BadRequest(validResult.ToString(" | "));
        }
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        Book[] result = _service.GetBooks();
        return Ok(result);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _service.DeleteBook(id);

        if (result)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> DeleteBooksByWriter(int id)
    {
        var result = await _service.DeleteBooksByWriter(id);

        if (result)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBook(Book book)
    {
        ValidationResult validResult = _validator.Validate(book);
        if (validResult.IsValid)
        {
            bool result = await _service.UpdateBook(book);

            if (result)
                return Ok(result);
            else
                return BadRequest(result);
        }
        else
        {
            return BadRequest(validResult.ToString(" | "));
        }
    }
}
