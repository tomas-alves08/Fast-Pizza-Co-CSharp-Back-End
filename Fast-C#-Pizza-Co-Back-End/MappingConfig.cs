using AutoMapper;
using Fast_C__Pizza_Co_Back_End.Models;
using Fast_C__Pizza_Co_Back_End.Models.DTO;

namespace Fast_C__Pizza_Co_Back_End
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PizzaOrder, PizzaOrderDTO>().ReverseMap();
            CreateMap<PizzaOrder, PizzaOrderCreateDTO>().ReverseMap();
            CreateMap<PizzaOrder, PizzaOrderUpdateDTO>().ReverseMap();
        }
    }
}
