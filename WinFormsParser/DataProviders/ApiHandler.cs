using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using GovernmentParse.Helpers;
using GovernmentParse.Models;
using Newtonsoft.Json;

namespace GovernmentParse.DataProviders
{
    public class ApiHandler
    {
        private readonly log4net.ILog _log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ICredentials _credentials;

        private readonly string _boundary;

        public ApiHandler(ICredentials credentials)
        {
            _boundary = "delimiter--------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            _credentials = credentials;
        }

        public SavedFileInfo Upload(string url, FileModel file, bool logProgress = false)
        {
            var additionalParameters = new NameValueCollection
            {
                {"Ip", file.Ip },
                {"Terminal", file.Terminal },
                {"NewCardCounter", file.NewCardCounter.ToString(CultureInfo.CurrentCulture) }
            };
            MemoryStream dataStream = new MemoryStream(file.Content);
            var responce = Upload(url, file.FileName, dataStream, additionalParameters);
            if (responce.Error == null)
            {
                if (file.FileType == ".xml")
                {
                    _log.SavedLaws(file.FileName);
                }
            }
            return responce;
        }

        public CollectionAfterCompare CompareFiles(string url, CollectionBeforeCompare files)
        {
            HttpWebRequest request = GetHttpWebRequest(url);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(files);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var responce = GetHttpWebResponse(request);
            return responce.Error != null
                ? new CollectionAfterCompare { Error = responce.Error }
                : JsonConvert.DeserializeObject<CollectionAfterCompare>(responce.Responce);
        }

        public SaveFilesErrorMessage SaveErrorMessage(string url, SaveFilesErrorMessage files)
        {
            HttpWebRequest request = GetHttpWebRequest(url);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(files);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var responce = GetHttpWebResponse(request);
            return responce.Error != null
                ? new SaveFilesErrorMessage { Error = responce.Error }
                : JsonConvert.DeserializeObject<SaveFilesErrorMessage>(responce.Responce);
        }

        public CardId UpdateNoticeColumn(string url, CardId cardId)
        {
            HttpWebRequest request = GetHttpWebRequest(url);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(cardId);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var responce = GetHttpWebResponse(request);
            return responce.Error != null
                ? new CardId { Error = responce.Error }
                : JsonConvert.DeserializeObject<CardId>(responce.Responce);
        }


        public SavedFileInfo Upload(string url, string fileName, MemoryStream dataStream, NameValueCollection nameValues)
        {
            HttpWebRequest request = GetHttpWebRequest(url);
            WriteAuthenticationToRequest(request);

            dataStream.Position = 0;
            using (MemoryStream outputStream = new MemoryStream())
            {
                WriteNameValuesToStream(outputStream, nameValues);

                WriteDataContentToStream(outputStream, dataStream, fileName);

                WriteToHttpStream(request, outputStream);
            }
            var responce = GetHttpWebResponse(request);
            return responce.Error != null 
                ? new SavedFileInfo { Error = responce.Error } 
                : JsonConvert.DeserializeObject<SavedFileInfo>(responce.Responce);
        }

        private ResponceFromApi GetHttpWebResponse(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        streamReader.Close();
                        httpResponse.Close();
                        return new ResponceFromApi {Responce = result };
                    }
                }
            }
            catch (WebException ex)
            {
                using (var streamReader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    var error = streamReader.ReadToEnd();
                    return new ResponceFromApi { Error = new ErrorModel { ErrorMsg = "Get responce from api error:" + ex.Message + "\n StackTrace:" + ex.StackTrace + "\n детали: " + error }};
                }
            }
        }

        private HttpWebRequest GetHttpWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = url.Contains("SaveFile") ? $"multipart/form-data; boundary={_boundary}" : "application/json";
            request.Method = "POST";
            return request;
        }

        private void WriteAuthenticationToRequest(HttpWebRequest request)
        {
            var user = _credentials.GetCredential(request.RequestUri, "Basic");
            string auth = $"{user.UserName}:{user.Password}";

            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(auth)));
        }

        private void WriteEndBoundaryToStream(MemoryStream stream)
        {
            WriteBoundaryToStream(stream, "--");
        }

        private void WriteBoundaryToStream(MemoryStream stream, string endDeliminator)
        {
            WriteToStream(stream, Encoding.ASCII, $"{_boundary}{endDeliminator}");
        }

        private void WriteDataContentToStream(MemoryStream outputStream, MemoryStream inputStream, string fileName)
        {
            // добавляем в начало потока разделитель
            WriteBoundaryToStream(outputStream, Environment.NewLine);

            string formName = "uploaded";
            string contentType = "application/octet-stream";

            WriteToStream(outputStream, Encoding.UTF8, $"Content-Disposition: form-data; name=\"{formName}\"; filename=\"{fileName}\"{Environment.NewLine}");
            WriteToStream(outputStream, Encoding.UTF8, string.Format("Content-Type: {0}{1}{1}", contentType, Environment.NewLine));

            byte[] buffer = new byte[inputStream.Length];
            int bytesRead;

            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
            }

            // добавляем новую строку перед финальным разделителем 
            WriteToStream(outputStream, Encoding.ASCII, Environment.NewLine);

            // добавляем в эту строку финальный разделитель
            WriteEndBoundaryToStream(outputStream);
        }

        private void WriteNameValuesToStream(MemoryStream stream, NameValueCollection nameValues)
        {
            foreach (string name in nameValues.Keys)
            {
                WriteBoundaryToStream(stream, Environment.NewLine);

                WriteToStream(stream, Encoding.UTF8, string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                WriteToStream(stream, Encoding.UTF8, nameValues[name] + Environment.NewLine);
            }
        }

        private void WriteToHttpStream(HttpWebRequest request, MemoryStream outputStream)
        {
            request.ContentLength = outputStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                outputStream.Position = 0;

                byte[] tempBuffer = new byte[outputStream.Length];
                outputStream.Read(tempBuffer, 0, tempBuffer.Length);
                outputStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
            }
        }

        private void WriteToStream(MemoryStream stream, Encoding encoding, string output)
        {
            byte[] headerbytes = encoding.GetBytes(output);
            stream.Write(headerbytes, 0, headerbytes.Length);
        }

    }
}