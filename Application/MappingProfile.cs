using Application.Category;
using Application.Product;
using AutoMapper;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create Mapping
            CreateMap<Product.Create.Command, Domain.Models.Product>()
            .AfterMap((soure,destination)=>
            {
                destination.isDeleted = false;
            });
            
            CreateMap<Category.Create.Command, Domain.Models.Category>()
            .AfterMap((soure,destination)=>
            {
                destination.isDeleted = false;
            });

            // List Mapping
            CreateMap<Domain.Models.Product, ProductDto>();
            CreateMap<Domain.Models.Category, CategoryDto>();

            //Update Mapping
            CreateMap<Product.Update.Command, Domain.Models.Product>();
            CreateMap<Category.Update.Command, Domain.Models.Category>();
        }
    }
}
