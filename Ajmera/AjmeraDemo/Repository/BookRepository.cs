using AjmeraDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraDemo.Repository
{
    public class BookRepository : IBookRepository
    {
        private IBookDBContext _dbcontext;

        public BookRepository(IBookDBContext dBContext)
        {
            _dbcontext = dBContext;
        }

        public async Task<Guid> Create(TblBook tblBook)
        {
            try
            {
                _dbcontext.TblBooks.Add(tblBook);
                await _dbcontext.SaveChanges();
                return tblBook.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<TblBook>> GetAll()
        {
            try
            {
                var employees = await _dbcontext.TblBooks.ToListAsync<TblBook>();
                return employees;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TblBook> GetById(Guid id)
        {
            try
            {
                var book = await _dbcontext.TblBooks.Where(b => b.Id == id).FirstOrDefaultAsync();
                return book;
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Update(Guid id, TblBook tblBook)
        {
            try
            {
                var book = await _dbcontext.TblBooks.Where(b => b.Id == id).FirstOrDefaultAsync();
                if (book == null) return "Book does not exists";

                book.Name = tblBook.Name;
                book.AuthorName = tblBook.AuthorName;

                await _dbcontext.SaveChanges();
                return "book details successfully modified";
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var book = _dbcontext.TblBooks.Where(b => b.Id == id).FirstOrDefault();
                if (book == null) return "Book does not exists";

                _dbcontext.TblBooks.Remove(book);
                await _dbcontext.SaveChanges();
                return "Book details deleted successfully";
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

    }
}
