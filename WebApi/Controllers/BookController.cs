﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            try
            {
                var result = query.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        //Books/id
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new(_context, _mapper);
            GetBookQueryValidator validator = new();         
            try
            {
                query.BookId = id;
                validator.ValidateAndThrow(query);
                result = query.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(command);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new();
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new(_context);
            DeleteBookCommandValidator validator = new();
            try
            {
                command.BookId = id;
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok("Kitap başarı ile silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
