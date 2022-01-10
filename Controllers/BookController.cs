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
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper mapper;

        public BookController(AppDbContext context,
        IMapper mapper)
        {
            this._appDbContext = context;
            this.mapper = mapper;
        }

        //GET ALL BOOKS
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> Get()
        {
            var books = await _appDbContext.Book.ToListAsync();
            return mapper.Map<List<BookDTO>>(books);
        }

        //GET ONE BOOK BY ID
        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            var book = await _appDbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null) return NotFound("Book not found");
            return mapper.Map<BookDTO>(book);
        }

        //POST NEW BOOK
        [HttpPost]
        public async Task<ActionResult> Post(BookCreationDTO book)
        {
            var newBook = mapper.Map<BookEntity>(book);
            _appDbContext.Add(newBook);
            await _appDbContext.SaveChangesAsync();
            var dto = mapper.Map<BookDTO>(newBook);
            return new CreatedAtRouteResult("GetBookById", new { id = newBook.Id }, dto);

        }

        //UPDATE BOOK
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, BookCreationDTO book)
        {
            var bookUpdate = await _appDbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (bookUpdate == null) return NotFound("Book not found");
            mapper.Map(book, bookUpdate);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

        //DELETE BOOK
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookDelete = await _appDbContext.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (bookDelete == null) return NotFound("Book not found");
            _appDbContext.Entry(bookDelete).State = EntityState.Deleted;
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}