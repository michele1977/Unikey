using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using MasterDetail.Business;
using MasterDetail.Models.DTO;
using Microsoft.Ajax.Utilities;

namespace MasterDetail.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookController : ApiController
    {
        readonly AuthorService _authorService = new AuthorService();

        [HttpGet]
        public JsonResult<List<BookDTO>> GetBooks(int authorId)
        {
            var selectedBooks = _authorService.GetAllBooksOfAuthor(authorId)
                .Select(x => new BookDTO(x)).ToList();
            return Json(selectedBooks);
        }
        // GET: Author

    }
}