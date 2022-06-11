using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            // Известно, что товара с номером 42 не существует... и будет возвращено null.
            var thing = _context.Products.Find(42);

            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            // Известно, что товара с номером 42 не существует... и будет возвращено null.
            var thing = _context.Products.Find(42);
            
            // Известно, что thing = null, а значит метод ToString() сгенерирует ошибку сервера.
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}