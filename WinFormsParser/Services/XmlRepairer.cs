using System;
using System.IO;
using System.Reflection;
using System.Text;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.Services
{
    public class XmlRepairer
    {
        private static readonly log4net.ILog Log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// метод исправляет поврежденные завершающие теги xml файла
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public static XmlRepairResult RepairXmlFile(string filePath)
        {
            try
            {
                var index = -1;
                byte[] fileEndBuffer, fileBeginBuffer;

                //читаем файл и формируем два временных массива для нового файла
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var fileEndStr = string.Empty;
                    var position = fileStream.Length;
                    fileEndBuffer = new byte[GetBufferSize(fileStream.Length)];
                    while (index == -1 && position >= fileEndBuffer.Length)
                    {
                        //устанавливаем начальную позицию чтения с потока
                        position -= fileEndBuffer.Length;
                        fileStream.Seek(position, SeekOrigin.Begin);
                        //читаем поток
                        fileStream.Read(fileEndBuffer, 0, fileEndBuffer.Length);
                        //переводим массив байт в строку
                        fileEndStr = Encoding.GetEncoding("windows-1251").GetString(fileEndBuffer);
                        //ищем индекс входжения искомой строки
                        index = fileEndStr.LastIndexOf("<bill", StringComparison.Ordinal);
                    }
                    if (index == -1)
                        throw new Exception("Не вдається знайти позицію '<bil' у пошкодженому файлі");
                    //заполняем буфер fileEndBuffer измененной частью файла
                    fileEndBuffer = Encoding.GetEncoding(1251).GetBytes(fileEndStr.Substring(0, index) + "</bills>");
                    //заполняем буфер fileBeginBuffer неизменной частью файла
                    fileStream.Seek(0, SeekOrigin.Begin);
                    fileBeginBuffer = new byte[position];
                    fileStream.Read(fileBeginBuffer, 0, fileBeginBuffer.Length);
                }

                //перезаписываем файл
                using (FileStream fileStream = new FileStream(filePath, FileMode.Truncate))
                {
                    fileStream.Write(fileBeginBuffer, 0, fileBeginBuffer.Length);
                    fileStream.Write(fileEndBuffer, 0, fileEndBuffer.Length);
                }

                return new XmlRepairResult { IsSuccess = true };
            }
            catch (Exception e)
            {
                Log.Error($"RepairXmlFile.\n{e.Message}\nStackTrace:{e.StackTrace}");
                return new XmlRepairResult { Error = new ErrorModel { ErrorMsg = e.Message, Operation = "RepairXmlFile" }, IsSuccess = false };
            }
        }

        private static long GetBufferSize(long fileLenght)
        {
            return fileLenght < 71680 ? fileLenght : 71680;
        }
    }
}
