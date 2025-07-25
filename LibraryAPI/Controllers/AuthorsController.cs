using AutoMapper;
using FluentValidation;
using Library.Aplication.DTOs;
using Library.Aplication.DTOs.Authors;
using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAuthorDto> _validator;
    private readonly IValidator<PaginatedDto> _paginatedValidator;

    public AuthorsController(IAuthorRepository repository,
        IMapper mapper,
        IValidator<CreateAuthorDto> validator,
        IValidator<PaginatedDto> paginatedValidaor)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
        _paginatedValidator = paginatedValidaor;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAuthorDto authorDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(authorDto, cancellationToken);
        if (validationResult.IsValid)
        {
            //var authorToCreate = _mapper.Map<Author>(authorDto);
            //authorToCreate.Books = _mapper.Map<List<Book>>(authorDto.Books)
            await _repository.CreateAuthor(_mapper.Map<Author>(authorDto), cancellationToken);
            return Created();
        }

        return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginatedDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _paginatedValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var authors = await _repository.GetAuthors(dto.Page, dto.PageSize, cancellationToken);
        var authorsDtos = _mapper.Map<List<AuthorDto>>(authors.Items);
        var result = new PaginatedList<AuthorDto>(authorsDtos, dto.Page, dto.PageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var author = await _repository.GetAuthorById(id, cancellationToken);
        if (author == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<AuthorDto>(author));
    }

    [HttpPut]
    public async Task<IActionResult> Put(AuthorDto authorDto, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(authorDto, cancellationToken);
        if (validationResult.IsValid)
        {
            await _repository.UpdateAuthor(_mapper.Map<Author>(authorDto), cancellationToken);
            return Created();
        }

        return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAuthor(id, cancellationToken);
        return Ok();
    }
}
