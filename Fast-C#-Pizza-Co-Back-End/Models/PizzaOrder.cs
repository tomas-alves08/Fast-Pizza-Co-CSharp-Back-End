using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fast_C__Pizza_Co_Back_End.Models
{
    public class PizzaOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<PizzaObj> PizzaArr { get; set; }
        public int TotalCost { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
