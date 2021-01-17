using AutoMapper;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DTO.RealitycsClient.EntityMapping
{
    public class RealitycsClientEntityMapping : Profile
    {

        public RealitycsClientEntityMapping()
        {
           
            CreateMap<tbl_Master_ClientInformation, DTO_ClientInformation>()
                .ForMember(dest => dest.ClientID, opts => opts.MapFrom(src => src.PK_ClientID))
                .ForMember(dest => dest.ContactEmail, opts => opts.MapFrom(src => src.ContactEmailID))
                .ForMember(dest => dest.MultiSelect, opts => opts.MapFrom(src => src.MultiSelect.Split(',', StringSplitOptions.RemoveEmptyEntries)));

            CreateMap<DTO_ClientInformation, tbl_Master_ClientInformation>()
                .ForMember(dest => dest.PK_ClientID, opts => opts.MapFrom(src => src.ClientID))
                .ForMember(dest => dest.ContactEmailID, opts => opts.MapFrom(src => src.ContactEmail))
                .ForMember(dest => dest.MultiSelect, opts => opts.MapFrom(src => string.Join(',', src.MultiSelect)));

            CreateMap<ViewUserList, ClientUserDTO>();
             
        }
    }
}
