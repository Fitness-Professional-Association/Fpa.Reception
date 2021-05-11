﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reception.fitnesspro.ru.Misc;

namespace reception.fitnesspro.ru.Controllers.Constraint
{
    [Route("[controller]")]
    [TypeFilter(typeof(ResourseLoggingFilter))]
    [TypeFilter(typeof(LoggedResultFilterAttribute))]
    [ApiController]
    public class ConstraintController : ControllerBase
    {
        private readonly IAppContext context;

        public ConstraintController(IAppContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetByKeys")]
        public async Task<ActionResult<IEnumerable<Domain.Constraint>>> GetByKeys(IEnumerable<Guid> constraintKeys)
        {
            if (constraintKeys == default)
            {
                ModelState.AddModelError(nameof(constraintKeys), "Ключи запроса не указаны");
                return BadRequest(ModelState);
            }

            var result = context.Constraint.Get(constraintKeys);

            if (result == default) return NoContent();

            return Ok(result.ToList());
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Domain.Constraint>>> GetAll()
        {
            var result = context.Constraint.GetAll();

            if (result == default) return NoContent();

            return Ok(result.ToList());
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddConstraint(Domain.Constraint constraint)
        {
            if (constraint.Validate() != true) return BadRequest("Для ограничения не указана дисциплина");

            context.Constraint.Store(constraint);

            return Ok();
        }
    }
}
