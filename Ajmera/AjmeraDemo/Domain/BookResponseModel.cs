using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraDemo.Domain
{
    public class BookResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}
