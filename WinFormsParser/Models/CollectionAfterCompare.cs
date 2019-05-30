using System.Collections.Generic;

namespace GovernmentParse.Models
{
    public class CollectionAfterCompare : BaseModel
    {
       public List<string> File { get; set; }

       public decimal NewCardCounter { get; set; }
    }
}
