using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class Page<T> : BaseModel
    {
        public List<Record<T>> PageDetails { get; set; }

        public List<FileModel> Files { get; set; }
    }
}
