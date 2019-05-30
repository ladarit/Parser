using System.Collections.Generic;
using HtmlAgilityPack;

namespace GovernmentParse.Models
{
    public class TableInfo : Page<List<string>>
    {
        public HtmlNode Table { get; set; }

        public List<HtmlNode> Rows { get; set; }

        public string HtmlDocString { get; set; }
    }
}
