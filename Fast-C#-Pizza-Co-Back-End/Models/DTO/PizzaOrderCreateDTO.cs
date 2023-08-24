using System.ComponentModel.DataAnnotations;

namespace Fast_C__Pizza_Co_Back_End.Models.DTO
{
    public class PizzaOrderCreateDTO
    {
        [Required]
        public List<PizzaObj>? PizzaArr { get; set; }
        [Required]
        public int TotalCost { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
