using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fast_C__Pizza_Co_Back_End.Models
{
    public class PizzaObj
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int PizzaOrderId { get; set; }
        public PizzaOrder PizzaOrder { get; set; }
    }
}
