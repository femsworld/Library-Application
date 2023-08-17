using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Domain.Entities;

namespace WebApi.Business.Dto
{
    public class BookDto
    {
        public Guid Id { get; set;}
        public string Title { get; set; } = default!;
        public Genre Genre { get; set; }
        public List<string>? Images { get; set; }
        // public int Inventory { get; set; }
    }

    public class BookReadDto
    {
        public string Title { get; set; } = default!;
        public Genre Genre { get; set; }
        public List<string>? Images { get; set; }
    }
}