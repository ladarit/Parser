using System;

namespace GovernmentParse.Models
{
    [Serializable]
    public class ErrorModel
    {
        public string ErrorMsg { get; set; }

        public string ControlName { get; set; }

        public string Operation { get; set; }

        public string Status { get; set; }
    }
}
