using AutoMapper;
using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models;
using Fast_C__Pizza_Co_Back_End.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Fast_C__Pizza_Co_Back_End.Controllers
{
    [Route("api/PizzaOrder")]
    [ApiController]
    public class PizzaOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public PizzaOrderController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaOrderDTO>>> GetPizzaOrders() 
        {
            IEnumerable<PizzaOrder> OrderList = await _db.PizzaOrders
                                                        .Include(order => order.PizzaArr)
                                                        .OrderBy(order => order.DeliveryTime)
                                                        .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<PizzaOrderDTO>>(OrderList));
        }

        [HttpGet("id:int", Name = "GetPizzaOrder")]
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

            return Ok(_mapper.Map<PizzaOrderDTO>(pizzaOrder));
        }

        [HttpPost]
        public async Task<ActionResult<PizzaOrderCreateDTO>> CreatePizzaOrder([FromBody] PizzaOrderCreateDTO createDTO)
        {
         

            Debug.WriteLine(createDTO);
            try
            {

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }

            List<PizzaObj> pizzas = new List<PizzaObj>();
                
            foreach(PizzaObj pizza in createDTO.PizzaArr)
            {
                if(pizza.Quantity != 0)
                {
                   pizzas.Add(pizza);
                }
            }

            PizzaOrder model = _mapper.Map<PizzaOrder>(createDTO);

                var x = JsonConvert.SerializeObject(model);

            _db.PizzaOrders.Add(model);
            await _db.SaveChangesAsync();

            return Ok(createDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "I'm sorry. Have a problem to save your information related to " + ex);
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
        public async Task<ActionResult> UpdatePizzaOrder(int id, [FromBody] PizzaOrderUpdateDTO updateDTO)
        {
            if(updateDTO == null || id != updateDTO.Id || id == 0)
            {
                return BadRequest();
            }

            var existingPizzaOrder = await _db.PizzaOrders.AsNoTracking().FirstOrDefaultAsync(pizza=> pizza.Id == id);

            if (existingPizzaOrder == null)
            {
                return NotFound();
            }

            _mapper.Map(updateDTO, existingPizzaOrder);

            var existingPizzaObjs = _db.PizzaObj.Where(pizza => pizza.PizzaOrderId == id).ToList();

            foreach (var pizzaItem in existingPizzaObjs)
            {
                var pizzaToRemove = updateDTO.PizzaArr.FirstOrDefault(pizza=> pizza.Id == pizzaItem.Id);

                if(pizzaToRemove == null) 
                {
                    _db.PizzaObj.Remove(pizzaItem);
                }
            }

            foreach (var updatedPizzaObj in updateDTO.PizzaArr)
            {
                var existingPizzaObj = existingPizzaObjs.FirstOrDefault(pizza => pizza.Id == updatedPizzaObj.Id);

                if (updatedPizzaObj.Quantity > 0)
                {
                    if (updatedPizzaObj.Id == 0)
                    {
                        // Create a new PizzaObj if the Id is -1
                        var pizzaObjModel = _mapper.Map<PizzaObj>(updatedPizzaObj);
                        pizzaObjModel.PizzaOrderId = id;
                        _db.PizzaObj.Add(pizzaObjModel);
                    }
                    else if (existingPizzaObj != null)
                    {
                        // Update existing PizzaObj properties
                        _mapper.Map(updatedPizzaObj, existingPizzaObj);
                        _db.Entry(existingPizzaObj).State = EntityState.Modified;
                    }
                }
                else if (existingPizzaObj != null)
                {
                    // Remove the PizzaObj if Quantity is 0
                    _db.PizzaObj.Remove(existingPizzaObj);
                }
            };


            /*foreach (var updatedPizzaObj in updateDTO.PizzaArr)
            {
                var existingPizzaObj = existingPizzaObjs.FirstOrDefault(pizza => pizza.Id == updatedPizzaObj.Id);

                if (existingPizzaObj != null)
                {
                    existingPizzaObj.Name = updatedPizzaObj.Name;
                    existingPizzaObj.Price = updatedPizzaObj.Price;
                    existingPizzaObj.Quantity = updatedPizzaObj.Quantity;
                }

                if (updatedPizzaObj.Quantity > 0 && updatedPizzaObj.Id == -1)
                {
                    PizzaObj pizza = new PizzaObj();

                    pizza.Name = updatedPizzaObj.Name;
                    pizza.Price = updatedPizzaObj.Price;
                    pizza.Quantity = updatedPizzaObj.Quantity;
                    pizza.PizzaOrderId = updatedPizzaObj.PizzaOrderId;

                    _db.PizzaObj.Add(pizza);
                }
            };*/

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }


}
