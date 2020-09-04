using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ReturnModels.CommanModels
{
    public class ReturnModel : BaseReturnModel
    {       
        public ReturnModel()
        {
            this.Success = true;
            this.ErrorMessage = null;
            this.ErrorCode = null;
        }

        public ReturnModel(string ErrorMessage,string ErrorCode)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.Success = false;
        }
    }
}
