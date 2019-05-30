using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class DownloadResult<T> : BaseModel
    {
        public List<T> RecievedData { get; set; }
    }
}
