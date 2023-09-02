using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Fast_C__Pizza_Co_Back_End.Controllers
{
    [Route("api/PizzaData")]
    [ApiController]
    public class PizzaDataController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PizzaDataController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PizzaDataDTO>> GetPizzas()
        {
            return Ok(_db.PizzaData.ToList());
        }
    }
}
