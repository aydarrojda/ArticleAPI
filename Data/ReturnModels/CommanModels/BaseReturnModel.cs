﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ReturnModels.CommanModels
{
    public class BaseReturnModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool Success { get; set; }
    }
}
