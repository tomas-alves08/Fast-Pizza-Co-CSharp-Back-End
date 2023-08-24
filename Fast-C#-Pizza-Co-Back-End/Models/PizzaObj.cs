using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public PizzaOrder? PizzaOrder { get; set; }
    }
}
