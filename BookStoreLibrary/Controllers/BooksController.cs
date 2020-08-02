using AutoMapper;
using BookStoreLibrary.Controllers.Resources;
using BookStoreLibrary.Core;
using BookStoreLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBookRepository repository;
        private readonly UserManager<User> userManager;

        public BooksController(IMapper mapper, IUnitOfWork unitOfWork, 
            IBookRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await repository.GetBooks();
            var result = mapper.Map<IEnumerable<Book>, IEnumerable<BookWithAuthorsResource>>(books);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await repository.GetBook(id);
            return Ok(mapper.Map<Book, BookWithAuthorsResource>(book));
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookSaveResource bookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = mapper.Map<BookSaveResource, Book>(bookResource);
            book.User = userManager.GetUserAsync(HttpContext.User).Result;
            repository.Add(book);

            await unitOfWork.CompleteAsync();

            book = await repository.GetBook(book.ID);
            var result = mapper.Map<Book, BookWithAuthorsResource>(book);
            return Created(nameof(GetBook), result);
        }

        [Authorize(Policy = Policies.Moderator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookSaveResource bookResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = await repository.GetBook(id);
            mapper.Map<BookSaveResource, Book>(bookResource, book);

            await unitOfWork.CompleteAsync();

            book = await repository.GetBook(book.ID);
            var result = mapper.Map<Book, BookWithAuthorsResource>(book);
            return Accepted(result);
        }
        
        [Authorize(Policy = Policies.Moderator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await repository.GetBook(id);

            if (book == null)
                return NotFound();

            repository.Remove(book);
            await unitOfWork.CompleteAsync();

            return Accepted();
        }
    }
}
