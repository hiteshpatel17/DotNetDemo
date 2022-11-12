using System;
using System.Collections.Generic;

#nullable disable

namespace AjmeraDemo.Models
{
    public partial class TblBook
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}
