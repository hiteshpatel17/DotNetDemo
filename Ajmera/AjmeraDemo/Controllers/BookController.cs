using AjmeraDemo.Domain;
using AjmeraDemo.Models;
using AjmeraDemo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        public BookController(IBookRepository bookRepository, ILogger<BookController> logger,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            
            _mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<BookRequestModel, TblBook>().ReverseMap();
                cfg.CreateMap<BookResponseModel, TblBook>().ReverseMap();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> create([FromBody] BookRequestModel bookModel)
        {
            //IMapper mapper = _mapperConfiguration.CreateMapper();

            try
            {
                var tblBook = _mapper.Map<BookRequestModel, TblBook>(bookModel);
                Guid bookId = await _bookRepository.Create(tblBook);
                return Ok(bookId);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var books = await _bookRepository.GetAll();
                var booksResult = _mapper.Map<List<TblBook>, List<BookResponseModel>>(books);
                return new OkObjectResult(booksResult);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try { 
            var book = await _bookRepository.GetById(id);
            var booksResult = _mapper.Map<TblBook, BookResponseModel>(book);
            return Ok(booksResult);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update( BookResponseModel bookResponseModel)
        {
            try { 
            var tblBook = _mapper.Map<BookResponseModel, TblBook>(bookResponseModel);
            string resp = await _bookRepository.Update(tblBook.Id, tblBook);
            return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try { 
            var resp = await _bookRepository.Delete(id);
            return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
