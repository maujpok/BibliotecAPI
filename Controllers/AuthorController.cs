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
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public AuthorController(AppDbContext context,
            IMapper mapper)
        {
            this._appDbContext = context;
            this.mapper = mapper;
        }
        
        //GET ALL AUTHORS
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            var authors = await _appDbContext.Author.ToListAsync();
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        //GET ONE AUTHOR BY ID
        [HttpGet("{id}", Name = "GetAuthorById")]
        public async Task<ActionResult<AuthorDTO>> Get(int id)
        {
            var author = await _appDbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            if(author == null) return NotFound("Author not found");
            return mapper.Map<AuthorDTO>(author);
        }

        //POST NEW AUTHOR
        [HttpPost]
        public async Task<ActionResult> Post(AuthorCreationDTO author)
        {
            var newAuthor = mapper.Map<AuthorEntity>(author);
            _appDbContext.Add(newAuthor);
            await _appDbContext.SaveChangesAsync();
            var dto = mapper.Map<AuthorDTO>(newAuthor);
            return new CreatedAtRouteResult("GetAuthorById", new { id = newAuthor.Id }, dto);

        }

        //UPDATE AUTHOR
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, AuthorCreationDTO author)
        {
            var authorUpdate = await _appDbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            if (authorUpdate == null) return NotFound("Author not found");
            mapper.Map(author, authorUpdate);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE AUTHOR
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var authorDelete = await _appDbContext.Author.FirstOrDefaultAsync(a => a.Id == id);
            if (authorDelete == null) return NotFound("Author not found");
            _appDbContext.Entry(authorDelete).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}