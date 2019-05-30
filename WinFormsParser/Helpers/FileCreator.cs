using System.Text;
using System.Xml;
using GovernmentParse.Models;
using GovernmentParse.Services;

namespace GovernmentParse.Helpers
{
    public class FileCreator
    {
        public FileModel CreateFile<T>(T fileContent, string fileType, string fileName = null)
        {
            var netHelper = new NetHelper();
            return new FileModel
            {
                FileName = fileName ?? string.Empty,
                FileType = fileType,
                Content = EncodeFileContentByType(fileContent),
                Ip = netHelper.GetHostIp(),
                Terminal = netHelper.GetHostName(),
                Md5Hash = GetFileMd5HashByType(fileContent)
            };
        }

        private byte[] EncodeFileContentByType<T>(T fileContent)
        {
            return GetFileContentType<T>()
                ? fileContent as byte[]
                : Encoding.GetEncoding(1251).GetBytes(StringHandler.GetIndentXml(fileContent as XmlDocument));
        }

        private string GetFileMd5HashByType<T>(T fileContent)
        {
            return HashCalculator.CalculateMd5Hash(EncodeFileContentByType(fileContent)).ToUpper();
        }

        private bool GetFileContentType<T>()
        {
            return typeof(T) == typeof(byte[]);
        }
    }
}
