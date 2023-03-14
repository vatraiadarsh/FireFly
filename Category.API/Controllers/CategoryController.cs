using Category.API.DataTransferObjects;
using Category.API.Model;
using Category.API.Parameters;
using Category.API.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Category.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IValidator<CategoryDto> _validator;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IValidator<CategoryDto> validator, ICategoryRepository categoryRepository)
        {
            _validator = validator;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParameters categoryParameters)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(categoryParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));
            var categoryDto = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Notes = c.Notes,
                Attachments = c.Attachments,
            }).ToList();
            return Ok(categoryDto);
        }


        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Notes = category.Notes,
                Attachments = category.Attachments,
                CreatedAt = category.CreatedAt
            };
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return UnprocessableEntity(validationResult.Errors.Select(e => e.ErrorMessage));

            var categoryEntity = new CategoryItem
            {
                Name = category.Name,
                Notes = category.Notes,
                Attachments = category.Attachments
            };
            _categoryRepository.CreateCategory(categoryEntity);
            return CreatedAtRoute("CategoryById", new { id = categoryEntity.Id }, categoryEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryDto category)
        {
            var validationResult = await _validator.ValidateAsync(category);
            if (!validationResult.IsValid) return UnprocessableEntity(validationResult.Errors.Select(e => e.ErrorMessage));

            var categoryEntity = await _categoryRepository.GetCategoryByIdAsync(id);
            if (categoryEntity == null) return NotFound();

            categoryEntity.Name = category.Name;
            categoryEntity.Notes = category.Notes;
            categoryEntity.Attachments = category.Attachments;

            _categoryRepository.UpdateCategory(categoryEntity);
            return CreatedAtRoute("CategoryById", new { id = categoryEntity.Id }, categoryEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            _categoryRepository.DeleteCategory(category);
            return NoContent();
        }
    }
}