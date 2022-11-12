using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraDemo.Models
{
    public interface IBookDBContext
    {
        DbSet<TblBook> TblBooks { get; set; }

        Task<int> SaveChanges();
    }
}
