using AutoMapper;
using Fast_C__Pizza_Co_Back_End.Data;
using Fast_C__Pizza_Co_Back_End.Models;
using Fast_C__Pizza_Co_Back_End.Models.DTO;
using Fast_C__Pizza_Co_Back_End.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Fast_C__Pizza_Co_Back_End.Controllers
{
    [Route("api/PizzaOrder")]
    [ApiController]
    public class PizzaOrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IPizzaOrderRepository _dbPizzaOrder;
        private readonly IMapper _mapper;
        public PizzaOrderController(ApplicationDbContext db,IPizzaOrderRepository dbPizzaOrder, IMapper mapper)
        {
            _db = db;
            _dbPizzaOrder = dbPizzaOrder;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaOrderDTO>>> GetPizzaOrders() 
        {
            IEnumerable<PizzaOrder> OrderList = await _dbPizzaOrder.GetAllAsync();

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
                                    .FirstOrDefault(order => order.Id == id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PizzaOrderDTO>(pizzaOrder));
        }

        [HttpPost]
        public async Task<ActionResult<PizzaOrderCreateDTO>> CreatePizzaOrder([FromBody] PizzaOrderCreateDTO createDTO)
        {
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

            await _dbPizzaOrder.CreateAsync(model);

            return Ok(createDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "I'm sorry. Have a problem to save your information related to " + ex);
            }
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> DeletePizzaOrder(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var pizzaOrder = await _dbPizzaOrder.GetOneAsync(order => order.Id == id);

            if (pizzaOrder == null)
            {
                return NotFound();
            }

            await _dbPizzaOrder.RemoveAsync(pizzaOrder);

            return NoContent();
        }

        [HttpPut("id:int")]
        public async Task<ActionResult> UpdatePizzaOrder(int id, [FromBody] PizzaOrderUpdateDTO updateDTO)
        {
            if(updateDTO == null || id != updateDTO.Id || id == 0)
            {
                return BadRequest();
            }

            var existingPizzaOrder = await _db.PizzaOrders.AsNoTracking().FirstOrDefaultAsync(pizza => pizza.Id == id);

            if (existingPizzaOrder == null)
            {
                return NotFound();
            }

            var model = _mapper.Map(updateDTO, existingPizzaOrder);
            _db.Update(model);

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
                        var pizzaObjModel = _mapper.Map<PizzaObj>(updatedPizzaObj);
                        pizzaObjModel.PizzaOrderId = id;
                        _db.PizzaObj.Add(pizzaObjModel);
                    }
                    else if (existingPizzaObj != null)
                    {
                        _mapper.Map(updatedPizzaObj, existingPizzaObj);
                        _db.Entry(existingPizzaObj).State = EntityState.Modified;
                    }
                }
                else if (existingPizzaObj != null)
                {
                    _db.PizzaObj.Remove(existingPizzaObj);
                }
            };

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }


}
