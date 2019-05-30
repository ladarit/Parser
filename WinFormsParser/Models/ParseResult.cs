using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class ParseResult<T> : BaseModel
    {
        public List<T> XmlDocuments { get; set; }

        public List<FileModel> Files { get; set; }
    }
}
