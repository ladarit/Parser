using System;
using GovernmentParse.Models;

namespace GovernmentParse.Helpers
{
    public static class FormattedExceptionCreator
    {
        public static Exception CreateExc(ErrorModel error, string errorMsg = null, string controlName = null, string operation = null)
        {
            if(error == null)
                error = new ErrorModel();
            error.ErrorMsg = error.ErrorMsg ?? errorMsg;
            error.ControlName = error.ControlName ?? controlName;
            error.Operation = error.Operation ?? operation;
            Exception ex = new Exception();
            ex.Data.Add("Error", error);
            return ex;
        }
    }
}
