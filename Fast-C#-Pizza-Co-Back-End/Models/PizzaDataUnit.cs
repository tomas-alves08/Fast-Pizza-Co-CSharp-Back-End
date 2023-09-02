using System.ComponentModel.DataAnnotations;

namespace Fast_C__Pizza_Co_Back_End.Models
{
    public class PizzaDataUnit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Ingredients { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string PhotoName { get; set; } = string.Empty;
        public bool SoldOut { get; set; }
    }
}
