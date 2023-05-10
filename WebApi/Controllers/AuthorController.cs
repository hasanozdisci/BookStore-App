using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new(_context, _mapper);
            GetAuthorDetailQueryValidator validator = new();
            query.AuthorId = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new(_context, _mapper);
            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new(_context);
            DeleteAuthorCommandValidator validator = new();
            command.AuthorId = id;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Yazar başarı ile silindi");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new(_context);
            UpdateAuthorCommandValidator validator = new();
            command.AuthorId = id;
            command.Model = updatedAuthor;
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
