using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class SavedFiles : BaseModel
    {
        public List<SavedFileInfo> SavedFilesInfo { get; set; }
    }
}
