using System;
using System.Collections.Generic;
using System.Reflection;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using HtmlAgilityPack;

namespace GovernmentParse.Services
{
    public class LawsTableCreator
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TableInfo CreateLawsTable(List<TableRow> laws)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                List<HtmlNode> rows = new List<HtmlNode>();
                foreach (var law in laws)
                {
                    var row = doc.CreateElement("tr");
                    for (int i = 0; i < law.Cells.Count; i++)
                    {
                        var td = doc.CreateElement("td");
                        if (i == 0)
                        {
                            var a = doc.CreateElement("a");
                            a.SetAttributeValue("href", law.Cells[i]);
                            a.InnerHtml = law.Cells[i + 1];
                            td.AppendChild(a);
                        }
                        if (i > 1)
                            td.InnerHtml = law.Cells[i];
                        if (!string.IsNullOrEmpty(td.InnerHtml))
                            row.AppendChild(td);
                    }
                    if (!string.IsNullOrEmpty(row.InnerHtml))
                        rows.Add(row);
                }
                return new TableInfo { Rows = rows };

            }
            catch (Exception ex)
            {
                _log.Error($"CreateFileFromXml.\n{ex.Message}\nStackTrace:{ex.StackTrace}");
                return new TableInfo { Error =new ErrorModel {ErrorMsg = ex.Message}, Operation = "CreateLawsTable", ControlName = "UpdateLaws" }; 
            }
        }
    }
}
