namespace CRUD_Entity_Framework_Core_MVC_01.Helpers;

using AutoMapper;
using CRUD_Entity_Framework_Core_MVC_01.Entities;
using CRUD_Entity_Framework_Core_MVC_01.Models.Karaoke;

public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
        //CreateRequest -> Song
        CreateMap<CreateRequest, Song>();

        //UpdateRequest -> Song
        CreateMap<UpdateRequest, Song>();
            .ForAllMembers(x => x.Condition((src, dest, prop) => {
                //ignore both null & empty string properties
                if (prop == null) return false;
                if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                //ignore null role
                if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                return true;
            }))
    }
}