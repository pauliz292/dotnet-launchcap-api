using Application.Product;
using Application.Property;
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

            CreateMap<Property.Create.Command, Domain.Models.Property>();

            // List Mapping
            CreateMap<Domain.Models.Product, ProductDto>();
            CreateMap<Domain.Models.Property, PropertyDto>();

            //Update Mapping
            CreateMap<Product.Update.Command, Domain.Models.Product>();
            // CreateMap<Property.Update.Command, Domain.Models.Property>();
        }
    }
}
