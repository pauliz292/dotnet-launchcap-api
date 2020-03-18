using Application.Borrower;
using Application.Property;
using AutoMapper;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create Mapping
            // CreateMap<Product.Create.Command, Domain.Models.Product>()
            // .AfterMap((soure,destination)=>
            // {
            //     destination.isDeleted = false;
            // });

            CreateMap<Property.Create.Command, Domain.Models.Property>();
            CreateMap<Borrower.Create.Command, Domain.Models.Borrower>();

            // List Mapping
            CreateMap<Domain.Models.Property, PropertyDto>();
            CreateMap<Domain.Models.Borrower, BorrowerDto>();

            //Update Mapping
            CreateMap<Property.Update.Command, Domain.Models.Property>();
            CreateMap<Borrower.Update.Command, Domain.Models.Borrower>();
        }
    }
}
