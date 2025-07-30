using AutoMapper;
using Library.Aplication.DTOs;
using Library.Aplication.DTOs.Categories;
using Library.Aplication.Interfaces;
using Library.Domain.Common;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var categoryToCreate = _mapper.Map<Category>(categoryDto);
        await _repository.CreateCategory(categoryToCreate, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = categoryToCreate.Id }, categoryToCreate);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginatedDto dto, CancellationToken cancellationToken)
    {

        var categories = await _repository.GetCategorys(dto.Page, dto.PageSize, cancellationToken);
        var categoriDtos = _mapper.Map<List<CategoryDto>>(categories.Items);
        var result = new PaginatedList<CategoryDto>(categoriDtos, dto.Page, dto.PageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetCategoryById(id, cancellationToken);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpPut]
    public async Task<IActionResult> Put(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        await _repository.UpdateCategory(_mapper.Map<Category>(categoryDto), cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteCategory(id, cancellationToken);
        return Ok();
    }
}
