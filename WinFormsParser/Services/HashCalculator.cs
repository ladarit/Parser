using System.Security.Cryptography;
using System.Text;

namespace GovernmentParse.Services
{
    public static class HashCalculator
    {
        public static string CalculateMd5Hash<T>(T file)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, GetBytesArray(file));
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, byte[] document)
        {
            var data = md5Hash.ComputeHash(document);
            StringBuilder sb = new StringBuilder();
            foreach (byte t in data)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        private static byte[] GetBytesArray<T>(T file)
        {
            return typeof(T) != typeof(byte[]) ? Encoding.GetEncoding(1251).GetBytes(file.ToString()) : file as byte[];
        }
    }
}
