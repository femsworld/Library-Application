using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities
{
    public class Author : BaseEnity
    {
        public string Name { get; set; } = default!;
        public List<Book> Books { get; set; } = default!;
    }
}