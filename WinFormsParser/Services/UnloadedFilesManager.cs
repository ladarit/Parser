//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using GovernmentParse.Helpers;
//using GovernmentParse.Models;
//using GovernmentParse.Parsers;
//using Newtonsoft.Json;

//namespace GovernmentParse.Services
//{
//    class HtmlDocModel
//    {
//        public string HtmlDocString { get; set; }

//        public List<string> XPathList { get; set; }

//        public string ControlName { get; set; }
//    }

//    public class UnloadedFilesManager
//    {
//        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

//        private readonly string _fileDirectory = AppDomain.CurrentDomain.BaseDirectory;

//        private string _dontDownloadedFile;

//        private string DontDownloadedFile
//        {
//            get { return _dontDownloadedFile; }
//            set { _dontDownloadedFile = Path.Combine(_fileDirectory, $"Unloaded{value}.json"); }
//        }

//        public void SaveUnloadedRowsToFile(string controlName, List<string> dontDownloadedRows, string htmlDocString)
//        {
//            DontDownloadedFile = controlName;
//            var htmlDocModel = new HtmlDocModel
//            {
//                HtmlDocString = htmlDocString,
//                XPathList = dontDownloadedRows,
//                ControlName = controlName
//            };
//            using (StreamWriter file = File.CreateText(DontDownloadedFile))
//            {
//                JsonSerializer serializer = new JsonSerializer();
//                serializer.Serialize(file, htmlDocModel);
//            }
//        }

//        public bool CheckForUnloadedRows(string controlName)
//        {
//            DontDownloadedFile = controlName;
//            return File.Exists(DontDownloadedFile);
//        }

//        public TableInfo GetUnloadedRows()
//        {
//            var rows = File.ReadAllText(DontDownloadedFile);
//            var htmlDocModel = JsonConvert.DeserializeObject<HtmlDocModel>(rows);
//            var document = Converter.ConvertToHtmlDocument(htmlDocModel.HtmlDocString);
//            var docNode = document.DocumentNode;
//            var tableParser = new TableDataParser();
//            var tableInfo = tableParser.GetTable(docNode, htmlDocModel.ControlName);
//            tableInfo.Rows = tableParser.GetRowsByXpath(tableInfo.Table, htmlDocModel.ControlName, htmlDocModel.XPathList);
//            tableInfo.HtmlDocString = htmlDocModel.HtmlDocString;
//            return tableInfo;
//        }

//        public void DeleteFile(string controlName)
//        {
//            DontDownloadedFile = controlName;
//            File.Delete(DontDownloadedFile);
//        }

//        ///// <summary>
//        ///// Serializes an object.
//        ///// </summary>
//        ///// <param name="serializableObject"></param>
//        ///// <param name="fileName"></param>
//        //public void SerializeObject(HtmlNode serializableObject, string fileName)
//        //{
//        //    if (serializableObject == null) { return; }
//        //    var innerHtml = serializableObject.InnerHtml;

//        //    try
//        //    {
//        //        XmlDocument xmlDocument = new XmlDocument();
//        //        XmlSerializer serializer = new XmlSerializer(typeof(string));
//        //        using (MemoryStream stream = new MemoryStream())
//        //        {
//        //            serializer.Serialize(stream, innerHtml);
//        //            stream.Position = 0;
//        //            xmlDocument.Load(stream);
//        //            xmlDocument.Save(fileName);
//        //            stream.Close();
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        _log.Error(ex.Message);
//        //    }
//        //}


//        ///// <summary>
//        ///// Deserializes an xml file into an object list
//        ///// </summary>
//        ///// <param name="fileName"></param>
//        ///// <returns></returns>
//        //public HtmlNode DeSerializeObject(string fileName)
//        //{
//        //    if (string.IsNullOrEmpty(fileName))
//        //        return default(HtmlNode);
//        //    try
//        //    {
//        //        string objectOut;
//        //        XmlDocument xmlDocument = new XmlDocument();
//        //        xmlDocument.Load(fileName);
//        //        string xmlString = xmlDocument.OuterXml;

//        //        using (StringReader read = new StringReader(xmlString))
//        //        {
//        //            XmlSerializer serializer = new XmlSerializer(typeof(string));
//        //            using (XmlReader reader = new XmlTextReader(read))
//        //            {
//        //                objectOut = serializer.Deserialize(reader).ToString();
//        //                reader.Close();
//        //            }
//        //            read.Close();
//        //        }

//        //        return new HtmlNode(HtmlNodeType.Element, new HtmlDocument(), 0)
//        //        {
//        //            InnerHtml = objectOut
//        //        };
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        _log.Error(ex.Message);
//        //        return default(HtmlNode);
//        //    }
//        //}
//    }
//}
