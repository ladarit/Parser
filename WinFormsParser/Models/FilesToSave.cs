using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class FilesToSave : BaseModel
    {
        public List<FileModel> Files { get; set; }
    }
}
