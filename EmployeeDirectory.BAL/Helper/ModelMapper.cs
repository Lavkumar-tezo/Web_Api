using AutoMapper;
using EmployeeDirectory.BAL.Interfaces.Providers;
using EmployeeDirectory.DAL.Interfaces;
using EmployeeDirectory.DAL.Models;
namespace EmployeeDirectory.BAL.Helper
{
    public class ModelMapper:Profile
    {
        public ModelMapper()
        {
            CreateMap<Role, DTO.Role>().
                ForMember(dest => dest.Departments, act => act.MapFrom(src => src.Departments.Select(dept => dept.Name).ToArray())).
                ForMember(dest => dest.Locations, act => act.MapFrom(src => src.Locations.Select(loc => loc.Name).ToArray()));
            CreateMap<Employee, DTO.Employee>().
                ForMember(dest=>dest.Department,act=>act.MapFrom(dest=>dest.Department.Name)).
                ForMember(dest=>dest.Location,act=>act.MapFrom(dest=>dest.Location.Name)).
                ForMember(dest => dest.Project, act => act.MapFrom(dest => dest.Project.Name)).
                ForMember(dest => dest.Role, act => act.MapFrom(dest => dest.Role.Name));
        }

        //public Mapper InitMapper()
        //{
        //    var config= new MapperConfiguration(cfg =>
        //    {
        //        //cfg.CreateMap<Role, DTO.Role>().ForMember(dest=>dest.Departments,act=>act.MapFrom(src=>src.Departments.Select(dept=>dept.Name).ToArray())).ForMember(dest => dest.Locations, act => act.MapFrom(src => src.Locations.Select(loc => loc.Name).ToArray()));

        //    });
        //    var mapper = new Mapper(config);
        //    return mapper;

        //}
    }
}
