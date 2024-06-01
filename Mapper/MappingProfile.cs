using AutoMapper;
using Evaluation_venussoftop.Models;
namespace Evaluation_venussoftop.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, VMPatient>().ReverseMap();
        }
    }
}
