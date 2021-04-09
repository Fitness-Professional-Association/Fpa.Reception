﻿using Application;
using Domain;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using reception.fitnesspro.ru.Controllers.Reception.Converter;
using reception.fitnesspro.ru.Controllers.Reception.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reception.fitnesspro.ru.Controllers.Reception
{
    [Route("[controller]")]
    [ApiController]
    public class ReceptionController : ControllerBase
    {
        private readonly IAppContext context;

        public ReceptionController(IAppContext context)
        {
            this.context = context;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> Get()
        {
            var result = context.Reception.Get();

            var viewmodel = result.Select(x => ReceptionViewModelConverter.ConvertDomainViewModel(x));

            return Ok(viewmodel);
        }

        [HttpGet]
        [Route("GetByKey")]
        public async Task<ActionResult> Get(Guid rceptionKey)
        {
            var result = context.Reception.Get();

            var viewmodel = result.Select(x => ReceptionViewModelConverter.ConvertDomainViewModel(x));

            return Ok(viewmodel);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateReceptionViewModel model)
        {
            if (ModelState.IsValid == false) return BadRequest(model);

            var item = new Domain.Reception().ConvertFromType(ReceptionViewModelConverter.ConvertViewModelToDomain, model);

            context.Reception.Store(item);

            return Ok();
        }
    }

}
