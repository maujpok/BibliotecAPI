using AutoMapper;
using BibliotecAPI.Data;
using BibliotecAPI.DTOs;
using BibliotecAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public CategoriesController(AppDbContext context,
            IMapper mapper)
        {
            this._appDbContext = context;
            this.mapper = mapper;
        }

        // Get all
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> Get()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return mapper.Map<List<CategoryDTO>> (categories);
        }

        // Get one by id
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return NotFound("Category not found");
            }
            return mapper.Map<CategoryDTO>(category);
        }

        // Post new
        [HttpPost]
        public async Task<ActionResult> Post(CategoryCreationDTO category)
        {
            var newCategory = mapper.Map<CategoryEntity>(category);
            _appDbContext.Add(newCategory);
            await _appDbContext.SaveChangesAsync();
            var dto = mapper.Map<CategoryDTO>(newCategory);
            return new CreatedAtRouteResult("GetCategory", new {id = newCategory.Id}, dto);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoryCreationDTO category)
        {
            var categoryUpdate = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
           
            if(categoryUpdate == null)
            {
                return NotFound("Category not found");
            }

            mapper.Map(category, categoryUpdate);
            
            _appDbContext.Entry(categoryUpdate).State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDelete = await _appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(categoryDelete == null)
            {
                return NotFound("Category not found");
            }

            _appDbContext.Entry(categoryDelete).State = EntityState.Deleted;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}