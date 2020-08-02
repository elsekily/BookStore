using AutoMapper;
using BookStoreLibrary.Controllers.Resources;
using BookStoreLibrary.Core;
using BookStoreLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreLibrary.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthorRepository repository;
        private readonly UserManager<User> userManager;

        public AuthorsController(IMapper mapper, IUnitOfWork unitOfWork,
            IAuthorRepository repository, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await repository.GetAuthors();
            var result = mapper.Map<IEnumerable<Author>, IEnumerable<AuthorWithBooksResource>>(authors);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await repository.GetAuthor(id);
            return Ok(mapper.Map<Author, AuthorWithBooksResource>(author));
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorSaveResource authorResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = mapper.Map<AuthorSaveResource, Author>(authorResource);
            author.User = userManager.GetUserAsync(HttpContext.User).Result;
            repository.Add(author);

            await unitOfWork.CompleteAsync();

            author = await repository.GetAuthor(author.ID);
            var result = mapper.Map<Author, AuthorWithBooksResource>(author);
            return Created(nameof(GetAuthor), result);
        }
        [Authorize(Policy = Policies.Moderator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorSaveResource authorResource)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = await repository.GetAuthor(id);
            mapper.Map<AuthorSaveResource, Author>(authorResource, author);

            await unitOfWork.CompleteAsync();

            author = await repository.GetAuthor(author.ID);
            var result = mapper.Map<Author, AuthorWithBooksResource>(author);
            return Accepted(result);
        }
        [Authorize(Policy = Policies.Moderator)]    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await repository.GetAuthor(id);

            if (author == null)
                return NotFound();

            repository.Remove(author);
            await unitOfWork.CompleteAsync();

            return Accepted();
        }
    }
}
