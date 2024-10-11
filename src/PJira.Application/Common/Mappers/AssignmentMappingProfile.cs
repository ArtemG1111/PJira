
using AutoMapper;
using PJira.Application.DTOs;
using PJira.Core.Models;

namespace PJira.Application.Common.Mappers
{
    public class AssignmentMappingProfile : Profile
    {
        public AssignmentMappingProfile()
        {
            CreateMap<Assignment, AssignmentDto>().ReverseMap();
        }
    }
}
