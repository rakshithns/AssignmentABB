using AssignmentABB.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentABB.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeToReturn>()
                .ForMember(d => d.EmployeeName, s => s.MapFrom(x => x.FirstName + " "+x.MiddleName +" "+ x.LastName));
            
            CreateMap<EmployeeToCreate, Employee>();
        }
    }
}
