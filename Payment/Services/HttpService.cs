using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Services
{
    internal class HttpService
    {
        /// <summary>
        /// 執行HttpClient Post
        /// </summary>
        /// <param name="url">Post網址</param>
        /// <param name="formContent">form參數</param>
        /// <returns></returns>
        internal static  string PostForm(string url, FormUrlEncodedContent formContent)
        {
            string responseBody = string.Empty;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsync(url, formContent).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        responseBody = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch
            {
                throw;
            }

            return responseBody;
        } 
    }
}
