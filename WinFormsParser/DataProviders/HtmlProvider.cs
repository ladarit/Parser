using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using GovernmentParse.Helpers;
using GovernmentParse.Models;

namespace GovernmentParse.DataProviders
{
    public static class HtmlProvider
    {
        private static readonly log4net.ILog Log = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static ResponceFromUrl<T> GetResponse<T>(string url, bool useUtf8Encoding = false, int? counter = 0)
        {
            Uri myUri = new Uri(url, UriKind.Absolute);
            try
            {
                object resp = null;
                Type typeParameterType = typeof(T);
                using (var client = new CustomWebClient())
                {
                    if (useUtf8Encoding)
                        client.Encoding = System.Text.Encoding.UTF8;
                    if (typeParameterType == typeof(byte[]))
                    {
                        var receivedData = client.DownloadData(myUri);
                        string fileType = null;
                        if (!string.IsNullOrEmpty(client.ResponseHeaders["Content-Disposition"]))
                        {
                            fileType = client.ResponseHeaders["Content-Disposition"];
                            fileType = fileType.Substring(fileType.LastIndexOf(".", StringComparison.Ordinal)).Replace("\"", "");
                        }
                        resp = new ResponceFromUrl<byte[]> { ReceivedData = receivedData, FileType = fileType ?? ".jpg" };
                    }
                    if (typeParameterType == typeof(string))
                        resp = new ResponceFromUrl<string> { ReceivedData = client.DownloadString(myUri) };
                }
                if (counter != null && counter > 0)
                    Log.Info($"Retrying download {myUri} SUCCESS on try: {counter}");
                return resp as ResponceFromUrl<T>;

                #region download by Task
                //var dowload = Task.Run(() =>
                //    {
                //        //object resp = null;
                //        using (var client = new CustomWebClient())
                //        {
                //            if (useUtf8Encoding)
                //                client.Encoding = System.Text.Encoding.UTF8;
                //            Type typeParameterType = typeof(T);
                //            if (typeParameterType == typeof(byte[]))
                //            {
                //                var receivedData = client.DownloadData(url);
                //                string fileType = null;
                //                if (!String.IsNullOrEmpty(client.ResponseHeaders["Content-Disposition"]))
                //                {
                //                    fileType = client.ResponseHeaders["Content-Disposition"];
                //                    fileType = fileType.Substring(fileType.LastIndexOf(".", StringComparison.Ordinal)).Replace("\"", "");
                //                }
                //                resp = new ResponceFromUrl<byte[]> { ReceivedData = receivedData, FileType = fileType ?? ".jpg" };
                //            }
                //            if (typeParameterType == typeof(string))
                //                resp = new ResponceFromUrl<string> {ReceivedData = client.DownloadString(url)};
                //        }
                //        return resp as ResponceFromUrl<T>;
                //    }
                //);
                //if (!dowload.Wait(TimeSpan.FromMilliseconds(6)))
                //    throw new Exception(url + " Час очікування підключення до сайту Верховної Ради сплив");
                //return dowload.Result;
                #endregion
            }
            catch (Exception ex)
            {
                var webEx = ex as WebException ?? ex.InnerException as WebException;
                var response = (HttpWebResponse)webEx?.Response;

                if (webEx != null && webEx.Status.ToString().Equals("Timeout") && counter <= 10)
                {
                    counter++;
                    var task = Task.Delay(30000).ContinueWith(_ => GetResponse<T>(url, useUtf8Encoding, counter));
                    Log.Info($"Try to restart download {myUri} with delay 30 sec ");
                    return task.Result;
                }

                if (webEx != null)
                    Log.Error(myUri + " " + webEx.Message + "\nStackTrace: " + webEx.StackTrace + "\ninnerEx: " + webEx.InnerException + "\nStatus: " + webEx.Status);
                else
                    Log.Error(myUri + " " + ex.Message + " StackTrace: " + ex.StackTrace);

                return new ResponceFromUrl<T>
                {
                    Error = new ErrorModel
                    {
                        ErrorMsg = response?.StatusCode == HttpStatusCode.NotFound ? HttpStatusCode.NotFound.ToString() : webEx?.Message ?? ex.Message,
                        Operation = "GetResponse" + " StackTrace: " + (webEx?.StackTrace ?? ex.StackTrace),
                        Status = webEx?.Status.ToString()
                    }
                };
            }
        }

        public static ResponceFromUrl<string> DownloadFileToLocalDisk(string url, string path)
        {
            try
            {
                Uri myUri = new Uri(url, UriKind.Absolute);
                using (var client = new CustomWebClient())
                {
                    client.DownloadFile(myUri, path);
                    return new ResponceFromUrl<string> { ReceivedData = path };
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + " StackTrace: " + ex.StackTrace);
                return new ResponceFromUrl<string> { Error = new ErrorModel { ErrorMsg = ex.Message, Operation = "DownloadFileToLocalDisk" } };
            }
        }

        public static bool Is404Except<T>(ResponceFromUrl<T> responce)
        {
            return responce.Error != null && responce.Error.ErrorMsg == HttpStatusCode.NotFound.ToString();
        }

        class CustomWebClient : WebClient
        {
            public CustomWebClient()
            {
//#if SKMU_server_ANYCPU
//                Proxy = new WebProxy("172.17.100.115", 3128);
//#else
                Proxy = WebRequest.GetSystemWebProxy();
//#endif
            }
            /// <summary>
            /// Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.
            /// </summary>
            /// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
            /// <returns>
            /// A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.
            /// </returns>
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest)
                {
                    (request as HttpWebRequest).KeepAlive = false;
                    (request as HttpWebRequest).Timeout = 60000;
                }
                return request;
            }
        }
    }
}
