﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, // Personel Growth
                PageCount = 200,
                PublishDate = new System.DateTime(2001, 06, 12)
            },
            new Book
            {
                Id = 2,
                Title = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new System.DateTime(2010, 05, 23)
            },
            new Book
            {
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new System.DateTime(2002, 12, 21)
            },
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            return BookList.OrderBy(x => x.Id).ToList<Book>();
        }

        [HttpGet("{id}")]
        //Books/id
        public Book GetBookById(int id)
        {
            return BookList.Where(b => b.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.FirstOrDefault(b => b.Title == newBook.Title);
            if(book is not null)
                return BadRequest();

            BookList.Add(newBook);
            return Ok(newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = BookList.FirstOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            return Ok(book);
        }

        [HttpDelete("{id}")] 
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.FirstOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();
            BookList.Remove(book);
            return Ok();
        }
    }
}
