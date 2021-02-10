﻿using System;

namespace reception.fitnesspro.ru.Controllers.Reception.ViewModel
{
    public class ConstraintViewModel
    {
        public Guid Program { get; set; }
        public Guid Group { get; set; }
        public Guid SubGroup { get; set; }

        public OptionViewModel Options { get; set; }
    }
}
