using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    public class BookDto
    {
        public string Title { get; set; } = default!;
        public List<Author> Authors { get; set; } = default!;
    }
}