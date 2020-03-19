using Application.Borrower;
using Application.Deal;
using Application.Guarantor;
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
            CreateMap<Guarantor.Create.Command, Domain.Models.Guarantor>();
            CreateMap<Deal.Create.Command, Domain.Models.Deal>();

            // List Mapping
            CreateMap<Domain.Models.Property, PropertyDto>();
            CreateMap<Domain.Models.Borrower, BorrowerDto>();
            CreateMap<Domain.Models.Guarantor, GuarantorDto>();
            CreateMap<Domain.Models.Deal, DealDto>();

            //Update Mapping
            CreateMap<Property.Update.Command, Domain.Models.Property>();
            CreateMap<Borrower.Update.Command, Domain.Models.Borrower>();
            CreateMap<Guarantor.Update.Command, Domain.Models.Guarantor>();
            CreateMap<Deal.Update.Command, Domain.Models.Deal>();
        }
    }
}
