using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(new Author
            {
                Name = "Eric",
                Surname = "Ries",
                DateOfBirth = new System.DateTime(1978, 09, 04)
            },
               new Author
               {
                   Name = "Charlotte",
                   Surname = "Perkins Gilman",
                   DateOfBirth = new System.DateTime(1860, 07, 03)
               },
               new Author
               {
                   Name = "Frank",
                   Surname = "Herbert",
                   DateOfBirth = new System.DateTime(1920, 10, 08)
               });
        }
    }
}
