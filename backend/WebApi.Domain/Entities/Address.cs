using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Domain.Entities
{
    public class Address : BaseEnity
    {
        public string Street { get; set; } = default!;
        public string State { get; set; } = default!;
        public string PostCode { get; set; } = default!;
    }
}