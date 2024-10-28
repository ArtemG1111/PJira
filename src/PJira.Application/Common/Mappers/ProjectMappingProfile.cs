

using AutoMapper;
using PJira.Application.DTOs;
using PJira.Core.Models;

namespace PJira.Application.Common.Mappers
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }

    }
}
