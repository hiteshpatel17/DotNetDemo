using AjmeraDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraDemo.Repository
{
    public interface IBookRepository
    {
        Task<Guid> Create(TblBook tblBook);
        Task<List<TblBook>> GetAll();
        Task<TblBook> GetById(Guid id);
        Task<string> Update(Guid id, TblBook tblBook);
        Task<string> Delete(Guid id);
    }
}
