using Microsoft.AspNetCore.Mvc;
using webApi.Services;
using webApi.DataClasses.Entities;
using FluentValidation.Results;
using FluentValidation;
using webApi.DataClasses.EntitiesCl;

namespace webApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WritersController : ControllerBase
{
    private IWritersService _service;
    private IValidator<WriterCl> _clValidator;
    private IValidator<Writer> _validator;

    public WritersController(IWritersService service, IValidator<WriterCl> clValidator, IValidator<Writer> validator)
    {
        _service = service;
        _clValidator = clValidator;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> AddWriter(WriterCl writerCl)
    {
        ValidationResult validResult = _clValidator.Validate(writerCl);
        if (validResult.IsValid)
        {
            var result = await _service.AddWriter(writerCl);

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

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> DeleteWriter(int id)
    {
        var result = await _service.DeleteWriter(id);

        if (result)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<IActionResult> GetWriterById(int id)
    {
        var result = await _service.GetWriters(id);

        if (result is not null)
            return Ok(result);
        else
            return BadRequest(result);
    }

    [HttpGet("{name:minlength(1)}")]
    public IActionResult GetWritersByName(string name)
    {
        Writer[] result = _service.GetWriters(name);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetWriters()
    {
        Writer[] result = _service.GetWriters();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateWriter(Writer writer)
    {
        ValidationResult validResult = _validator.Validate(writer);
        if (validResult.IsValid)
        {
            bool result = await _service.UpdateWriter(writer);

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
