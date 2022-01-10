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
    public class EditorialController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public EditorialController(AppDbContext context,
            IMapper mapper)
        {
            this._appDbContext = context;
            this.mapper = mapper;
        }

        //GET ALL
        [HttpGet]
        public async Task<ActionResult<List<EditorialDTO>>> Get()
        {
            var editorial = await _appDbContext.Editorial.ToListAsync();
            return mapper.Map<List<EditorialDTO>>(editorial);
        }

        //GET ONE BY ID
        [HttpGet("{id}", Name = "GetEditorialById")]
        public async Task<ActionResult<EditorialDTO>> Get(int id)
        {
            var editorial = await _appDbContext.Editorial.FirstOrDefaultAsync(a => a.Id == id);
            if (editorial == null) return NotFound("Editorial not found");
            return mapper.Map<EditorialDTO>(editorial);
        }

        //POST NEW
        [HttpPost]
        public async Task<ActionResult> Post(EditorialCreationDTO editorial)
        {
            var newEditorial = mapper.Map<EditorialEntity>(editorial);
            _appDbContext.Add(newEditorial);
            await _appDbContext.SaveChangesAsync();
            var dto = mapper.Map<EditorialDTO>(newEditorial);
            return new CreatedAtRouteResult("GetEditorialById", new { id = newEditorial.Id }, dto);

        }

        //UPDATE
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EditorialCreationDTO editorial)
        {
            var editorialUpdate = await _appDbContext.Editorial.FirstOrDefaultAsync(a => a.Id == id);
            if (editorialUpdate == null) return NotFound("Editorial not found");
            mapper.Map(editorial, editorialUpdate);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var editorialDelete = await _appDbContext.Editorial.FirstOrDefaultAsync(a => a.Id == id);
            if (editorialDelete == null) return NotFound("Editorial not found");
            _appDbContext.Entry(editorialDelete).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}