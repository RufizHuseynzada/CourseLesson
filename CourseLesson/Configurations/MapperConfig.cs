
using AutoMapper;
using CourseLesson.Data;
using CourseLesson.Models.Country;
using CourseLesson.Models.Library;
using CourseLesson.Models.Users;

namespace CourseLesson.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryMod>().ReverseMap();
            CreateMap<Country, GetCountriesMod>().ReverseMap();
            CreateMap<Country, GetCountryMod>().ReverseMap();
            CreateMap<Country, UpdateCountryMod>().ReverseMap();
            CreateMap<Library, GetLibrariesMod>().ReverseMap();
            CreateMap<Library, CreateLibraryMod>().ReverseMap();
            CreateMap<Library, GetLibraryMod>().ReverseMap();
            CreateMap<Library, UpdateLibraryMod>().ReverseMap();
            CreateMap<ApiUser, CreateUserMod>().ReverseMap();
            CreateMap<ApiUser, LogInMod>().ReverseMap();

        }
    }
}
