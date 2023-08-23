using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models;
using Fast_C__Pizza_Co_Back_End.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fast_C__Pizza_Co_Back_End.Controllers
{
    [Route("api/PizzaOrderApi")]
    [ApiController]
    public class PizzaOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PizzaOrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PizzaOrderDTO>> GetPizzaOrder() 
        {
            return Ok(_db.PizzaOrders.ToList());
        }

        [HttpGet("id:int", Name = "GetTodo")]
        public ActionResult<PizzaOrderDTO> GetPizzaOrder(int id) 
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var pizzaOrder = _db.PizzaOrders.FirstOrDefault(order=> order.Id == id);

            if(pizzaOrder == null)
            {
                return NotFound();
            }

            return Ok(pizzaOrder);
        }

        [HttpPost]
        public ActionResult<PizzaOrderCreateDTO> CreatePizzaOrder([FromBody] PizzaOrderCreateDTO orderDTO)
        {
            Console.WriteLine(orderDTO);
            if(orderDTO == null)
            {
                return BadRequest(orderDTO);
            }

            PizzaOrder model = new()
            {
                PizzaArr = orderDTO.PizzaArr,
                TotalCost = orderDTO.TotalCost,
                DeliveryTime = orderDTO.DeliveryTime,
            };

            _db.PizzaOrders.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("CreatePizzaOrder", orderDTO);
        }

        [HttpDelete("id:int")]
        public IActionResult DeletePizzaOrder(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var pizzaOrder = _db.PizzaOrders.FirstOrDefault(order => order.Id == id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            _db.PizzaOrders.Remove(pizzaOrder);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPut("id:int")]
        public ActionResult UpdatePizzaOrder(int id, [FromBody]PizzaOrderUpdateDTO orderDTO)
        {
            if(orderDTO == null || id != orderDTO.Id || id == 0)
            {
                return BadRequest();
            }

            PizzaOrder model = new()
            {
                Id = orderDTO.Id,
                PizzaArr = orderDTO.PizzaArr,
                TotalCost = orderDTO.TotalCost,
                DeliveryTime = orderDTO.DeliveryTime,
            };

            _db.PizzaOrders.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }


}
