using AutoMapper;
using Restaurant_Task1.DTO;
using Restaurant_Task1.Model;
using Restaurant_Task1.ModelView;

namespace Restaurant_Task1.Mapper
{
    public class Mapping : Profile
    {

        public Mapping()
        {
            CreateMap<RestaurantModelView, Restaurant>().ReverseMap();
            CreateMap<ResturantCreateDto, Restaurant>().ReverseMap();


            CreateMap<RestaurantMenuModelView, RestaurantMenu>().ReverseMap();
            CreateMap<ResturantMenuCreateDto, RestaurantMenu>().ReverseMap();


            CreateMap<CustomerViewModel, Customer>().ReverseMap();
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();

            CreateMap<OrderModelView, Order>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();


            CreateMap<RestaurantViewModel, RestaurantView>().ForMember(a => a.Name, x => x.MapFrom(a => a.RestaurantName))
                .ForMember(a => a.Expr1, x => x.MapFrom(a => a.MostPurchasedCustomer)).ReverseMap();



        }
    }
}
