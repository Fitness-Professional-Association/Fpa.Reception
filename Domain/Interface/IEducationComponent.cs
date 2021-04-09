﻿using Domain.Education;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IEducationComponent
    {
        Task<IEnumerable<Program>> GetAllPrograms();
        Task<IEnumerable<BaseInfo>> GetDisciplinesByKeys(IEnumerable<Guid> disciplineKeys);
        Task<IEnumerable<Program>> GetProgramsByDiscipline(Guid disciplineKey);
        Task<IEnumerable<Program>> GetProgramsByKeys(IEnumerable<Guid> programKeys);
        
        Task<IEnumerable<Program>> GetTeacherEducation(Guid employeeKey);
    }
}
