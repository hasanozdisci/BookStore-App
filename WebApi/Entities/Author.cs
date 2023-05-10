using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
