using AutoMapper;
using FluentValidation;
using Library.Aplication.DTOs;
using Library.Aplication.DTOs.Authors;
using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
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
            var authorToCreate = _mapper.Map<Author>(authorDto);
            await _repository.CreateAuthor(authorToCreate, cancellationToken);
            return CreatedAtAction(nameof(Get), new {id = authorToCreate.Id}, _mapper.Map<AuthorDto>(authorToCreate));
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
        var result = new PaginatedList<AuthorDto>(authorsDtos, dto.Page, authors.TotalPages);
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
            return Ok();
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
