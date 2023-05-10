using AutoMapper;
using System;
using System.Collections.Generic;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            var authorList = _context.Authors;
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return vm;
        }
    }
    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirthday { get; set; }
    }
}
