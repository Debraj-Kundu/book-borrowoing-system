using AutoMapper;
using BuisnessLayer.Domain;
using BuisnessLayer.BookAppServices.Interface;
using DataLayer.Entity;
using DataLayer.Repository.Interface;
using Book_Borrowing_System.DTO;
using Book_Borrowing_System.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedLayer.Core.Logging;
using System.Security.Claims;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IBookService BookService { get; }
        public IMapper Mapper { get; }
        public IFileService FileService { get; }
        public SharedLayer.Core.Logging.ILogger Logger { get; }

        public BookController(IBookService bookService, IMapper mapper, IFileService fileService, SharedLayer.Core.Logging.ILogger logger)
        {
            BookService = bookService;
            Mapper = mapper;
            FileService = fileService;
            Logger = logger;
        }
        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get()
        {
            var result = await BookService.GetAllBooks();
            var books = Mapper.Map<IEnumerable<BookDto>>(result.Data);
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            var result = await BookService.GetBookById(id);
            if (result.IsSuccess == false ||  result.Data == null)
                return NotFound();
            var book = Mapper.Map<BookDto>(result.Data);
            return Ok(book);
        }

        // GET api/<BookController>/abc
        [HttpGet("/api/[controller]/search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get([FromQuery]string? name, [FromQuery] string? author, [FromQuery] string? genre)
        {
            var result = await BookService.GetBookByParam(name, author, genre);
            if (result.IsSuccess == false || result.Data == null)
                return NotFound();
            var book = Mapper.Map<IEnumerable<BookDto>>(result.Data);
            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BookDto>> Post([FromForm]BookDto book)
        {
            
            if(book.ImageFile != null)
            {
                var fileResult = FileService.SaveImage(book.ImageFile);
                if(fileResult.Item1 == 1)
                {
                    book.BookImage = fileResult.Item2;
                }
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            book.Lent_By_User_id = int.Parse(userId);
            BookDomain bookToCreate = Mapper.Map<BookDomain>(book);
            var result = await BookService.CreateBook(bookToCreate);
            if(result.IsSuccess)
                return Created(nameof(Post), book);
            
            return BadRequest(result.MainMessage.Text);
        }

        [HttpPut("borrow/{bookId}")]
        [Authorize]
        public async Task<ActionResult> BorrowBook(int bookId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var borrowerId = int.Parse(id);
            var result = await BookService.BorrowBook(bookId, borrowerId);
            if (result.IsSuccess)
                return Created(nameof(BorrowBook), result.MainMessage.Text);

            return BadRequest(result.MainMessage.Text);
        }

        [HttpPut("return/{bookId}")]
        [Authorize]
        public async Task<ActionResult> ReturnBook(int bookId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var borrowerId = int.Parse(id);
            var result = await BookService.ReturnBook(bookId, borrowerId);
            if (result.IsSuccess)
                return Created(nameof(ReturnBook), result.MainMessage.Text);

            return BadRequest(result.MainMessage.Text);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(int id, [FromForm]BookUpdateDto book)
        {

            if (book.ImageFile != null)
            {
                var fileResult = FileService.SaveImage(book.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    book.BookImage = fileResult.Item2;
                }
            }
            //book.Id = id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var bookDomain = Mapper.Map<BookDomain>(book);
            var result = await BookService.UpdateBook(id, bookDomain, int.Parse(userId));

            if (result.IsSuccess)
                return Created(nameof(Put), result.MainMessage.Text);
            else if (result.MainMessage.Code == "401")
                return Unauthorized();

            return BadRequest(result.MainMessage.Text);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var bookToDelete = await BookService.GetBookById(id);
            FileService.RemoveImage(bookToDelete.Data.BookImage);
            var result = await BookService.RemoveBook(id);
            if (result.IsSuccess)
                return Ok();
            return NotFound();
        }
    }
}
