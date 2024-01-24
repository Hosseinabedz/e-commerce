using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        #region Ctor
        public BuggyController(StoreDbContext context)
        {
            _Context = context;
        }

        public StoreDbContext _Context { get; }
        #endregion

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _Context.Products.Find(42);
            if (thing == null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _Context.Products.Find(42);
            var thingToString = thing?.ToString();

            return Ok(new ApiResponse(500));
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadReauest() 
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
