﻿using Domain.Education;
using Domain.Model.Education;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IEducationComponent
    {
        Task<IEnumerable<ControlType>> GetControlTypesByKeys(IEnumerable<Guid> controlTypeKeys);
        Task<IEnumerable<BaseInfo>> GetDisciplinesByKeys(IEnumerable<Guid> disciplineKeys);
        Task<IEnumerable<Program>> GetProgramsByDiscipline(Guid disciplineKey);
        Task<IEnumerable<Program>> GetProgramsByKeys(IEnumerable<Guid> programKeys);        
        Task<IEnumerable<Group>> GetGroupsByKeys(IEnumerable<Guid> groupKeys);

        Task<IEnumerable<Program>> GetProgramInfo(Guid? teacherKey);     

        Task<Program> GetStudentEducation(Guid programKey);

        Task<IEnumerable<BaseInfo>> GetRates();

        Task<IEnumerable<BaseInfo>> GetTeachers(IEnumerable<Guid> teacherKeys);
    }
}
