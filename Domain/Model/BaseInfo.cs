﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BaseInfo
    {
        public Guid Key { get; set; }
        public string Title { get; set; }

        public BaseInfo()
        {}

        public BaseInfo(Guid key, string title)
        {
            Key = key;
            Title = title;
        }
    }
}