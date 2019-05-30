using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public class XmlValidator
    {
        private static readonly log4net.ILog Log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// метод проверяет xml файл на корректность (закрыт ли последний тег)
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        /// <param name="regSearchTerm">регул. выражение для поиска</param>
        public static XmlValidationModel IsXmlEndedCorrect(string filePath, string regSearchTerm)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    var array = new byte[50];
                    //устанавливаем позицию начала чтения с потока
                    fileStream.Seek(fileStream.Length - 50, SeekOrigin.Begin);
                    //читаем последние 50 байт потока
                    fileStream.Read(array, 0, 50);
                    //переводим массив байт в строку
                    return new XmlValidationModel { IsXmlValid = Regex.IsMatch(Encoding.GetEncoding("windows-1251").GetString(array), regSearchTerm) };
                }
            }
            catch (Exception e)
            {
                Log.Error($"IsXmlEndedCorrect.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new XmlValidationModel {Error = new ErrorModel {ErrorMsg = e.Message}};
            }
        }
    }
}
