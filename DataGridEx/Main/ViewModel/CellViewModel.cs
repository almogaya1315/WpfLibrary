﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.ViewModel
{
    public class CellViewModel
    {
        public CellViewModel(object value)
        {
            Value = value;
        }

        public object Value { get; set; }
    }
}
