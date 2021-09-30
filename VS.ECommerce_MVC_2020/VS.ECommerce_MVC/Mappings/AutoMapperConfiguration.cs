using AutoMapper;
using VS.ECommerce_MVC.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entity.Products, ProductViewModel>();
            });
        }

    }
}