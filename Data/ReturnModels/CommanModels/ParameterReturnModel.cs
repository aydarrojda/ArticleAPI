using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ReturnModels.CommanModels
{
   public class ParameterReturnModel<T> : BaseReturnModel
    {
        public T Model { get; set; }
        public ParameterReturnModel(T Model)
        {
            this.Success = true;
            this.ErrorMessage = null;
            this.ErrorCode = null;
            this.Model = Model;
        }

        public ParameterReturnModel(string ErrorMessage, string ErrorCode)
        {
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.Success = false;
            
        }
    }
}
