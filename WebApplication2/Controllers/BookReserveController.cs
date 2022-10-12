using BookReserveWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookReserveWeb
{
    [ApiController]
    [Route("Book")]
    public class BookReserveController : ControllerBase
    {
        private readonly ILogger<BookReserveController> _logger;

        public BookReserveController(ILogger<BookReserveController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Reserve")]
        public string ReserveBook(int id, string comment)
        {
            return "Created";
        }

        [HttpPost("RemoveReserve")]
        public string RemoveReservedStatus(int id)
        {
            return "Removed reserved status";
        }

        [HttpGet("AllReserved")]
        public IEnumerable<WebBook> GetAllReserved()
        {
            return new List<WebBook>();
        }

        [HttpGet("AllNotReserved")]
        public IEnumerable<WebBook> GetAllNotReserved()
        {
            return new List<WebBook>();

        }

        [HttpGet("StatusHistory")]
        public IEnumerable<StatusOfBook> GetStatusHistory(int id)   
        {
            return new List<StatusOfBook>();

        }
    }
}
