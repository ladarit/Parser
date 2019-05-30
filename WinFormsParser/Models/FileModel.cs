using System;

namespace GovernmentParse.Models
{
    public class FileModel : BaseModel, IDisposable
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public byte[] Content { get; set; }

        public string Md5Hash { get; set; }

        public string Terminal { get; set; }

        public string Ip { get; set; }

        public decimal NewCardCounter { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Array.Clear(Content, 0, Content.Length);
                Content = null;
                FileName = null;
                FileType = null;
                Md5Hash = null;
                Terminal = null;
                Ip = null;
            }
        }
    }
}
