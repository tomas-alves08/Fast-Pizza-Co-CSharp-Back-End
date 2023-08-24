using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models;
using Fast_C__Pizza_Co_Back_End.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

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
            return Ok(_db.PizzaOrders
                .Include(order => order.PizzaArr)
                .ToList());
        }

        [HttpGet("id:int", Name = "GetTodo")]
        public ActionResult<PizzaOrderDTO> GetPizzaOrder(int id) 
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var pizzaOrder = _db.PizzaOrders
                                    .Include(pizza => pizza.PizzaArr)
                                    .FirstOrDefault(order=> order.Id == id);

            if(pizzaOrder == null)
            {
                return NotFound();
            }

            return Ok(pizzaOrder);
        }

        [HttpPost]
        public async Task<ActionResult<PizzaOrderCreateDTO>> CreatePizzaOrder([FromBody] PizzaOrderCreateDTO orderDTO)
        {
            Debug.WriteLine(orderDTO);
            try
            {

            if (orderDTO == null)
            {
                return BadRequest(orderDTO);
            }

            PizzaOrder model = new()
            {
                PizzaArr = orderDTO.PizzaArr,
                TotalCost = orderDTO.TotalCost,
                DeliveryTime = orderDTO.DeliveryTime,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

                var x = JsonConvert.SerializeObject(model);

            _db.PizzaOrders.Add(model);
            await _db.SaveChangesAsync();

            return Ok(orderDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "I'm sorry. Have a problem to save your information.");
            }
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

            var existingPizzaOrder = _db.PizzaOrders.FirstOrDefault(pizza => pizza.Id == id);

            if(existingPizzaOrder == null)
            {
                return NotFound();
            }

            existingPizzaOrder.TotalCost = orderDTO.TotalCost;
            existingPizzaOrder.DeliveryTime = orderDTO.DeliveryTime;
            existingPizzaOrder.UpdateDate = DateTime.Now;

            var existingPizzaObjs = _db.PizzaObj.Where(pizza => pizza.PizzaOrderId == id).ToList();

            foreach(var updatedPizzaObj in orderDTO.PizzaArr)
            {
                var existingPizzaObj = existingPizzaObjs.FirstOrDefault(pizza => pizza.Id == updatedPizzaObj.Id);

                if(existingPizzaObj != null)
                {
                    existingPizzaObj.Name = updatedPizzaObj.Name;
                    existingPizzaObj.Price = updatedPizzaObj.Price;
                    existingPizzaObj.Quantity = updatedPizzaObj.Quantity;
                }
            }

            _db.SaveChanges();

            return NoContent();
        }
    }


}
