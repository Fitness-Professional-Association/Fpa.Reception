﻿using Application.Mappings;
using Domain.Interface;
using Mapster;
using Service.lC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lcService = Service.lC;

namespace Application.Component
{
    public class EducationComponent : IEducationComponent
    {
        private readonly Context lcService;

        public EducationComponent(Context lcService)
        {
            this.lcService = lcService;
        }

        public async Task<IEnumerable<Domain.Education.Program>> FindProgramByDiscipline(Guid disciplineKey)
        {
            var programManager = lcService.Program;

            var programs = await programManager.FilterByDiscipline(disciplineKey);
                await programManager.IncludeTeachers(programs);
                await programManager.IncludeGroups(programs);

            var groupManager = lcService.Group;
            var groups = programs.SelectMany(x => x.Groups);
                await groupManager.IncludeSubGroups(groups);

            var domain = programs.Adapt<IEnumerable<Domain.Education.Program>>();

            return domain;
        }
    }
}
