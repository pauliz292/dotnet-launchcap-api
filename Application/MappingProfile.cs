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

            // List Mapping
            CreateMap<Domain.Models.Product, ProductDto>();

            //Update Mapping
            CreateMap<Product.Update.Command, Domain.Models.Product>();
        }
    }
}
