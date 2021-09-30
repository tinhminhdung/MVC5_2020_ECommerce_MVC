using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class BaseApiClient
{
    //Convert Json to C# Classes Online
    // --> https://json2csharp.com/

   

    public async static Task<TResponse> GetAsync<TResponse>(string url)
    {
        using (var client = new HttpClient())
        {
            System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
            var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();

            client.BaseAddress = new Uri(Commond.ConfigHelper.GetByKey("BaseAddress"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            client.DefaultRequestHeaders.Add("Token", sessions);

            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializedObjList = (TResponse)JsonConvert.DeserializeObject(body,
                    typeof(TResponse));

                return myDeserializedObjList;
            }
            return JsonConvert.DeserializeObject<TResponse>(body);
        }
    }

    //public async static Task<T> CallAsync<T>(string url, HttpMethod httpMethod, string content)
    //{
    //    using (var httpClientHandler = new HttpClientHandler())
    //    {
    //        httpClientHandler.PreAuthenticate = true;
    //        httpClientHandler.UseDefaultCredentials = true;

    //        var basicCredCache = new CredentialCache();
    //        var basicCredentials = new NetworkCredential();
    //        basicCredCache.Add(new Uri(new Uri(Commond.ConfigHelper.GetByKey("BaseAddress")), url), "Basic", basicCredentials);
    //        httpClientHandler.Credentials = basicCredCache;

    //        using (var client = new HttpClient(httpClientHandler))
    //        {
    //            System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
    //            var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();

    //            client.BaseAddress = new Uri(Commond.ConfigHelper.GetByKey("BaseAddress"));
    //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
    //            client.DefaultRequestHeaders.Add("Token", sessions);

    //            using (var request = new HttpRequestMessage(httpMethod, url))
    //            {
    //                if (!string.IsNullOrWhiteSpace(content))
    //                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
    //                try
    //                {
    //                    var response = await client.SendAsync(request);
    //                    if (response.IsSuccessStatusCode)
    //                    {
    //                        string strResponse = await response.Content.ReadAsStringAsync();
    //                        return JsonConvert.DeserializeObject<T>(strResponse, new JsonSerializerSettings
    //                        {
    //                            ContractResolver = new CamelCasePropertyNamesContractResolver()
    //                        });
    //                    }
    //                    else
    //                    {
    //                        throw new Exception(response.Content.ToString());
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //            }
    //        }
    //    }
    //}

    public async static Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
    {
        using (var client = new HttpClient())
        {
            System.Web.HttpContext.Current.Session["Token"] = Commond.ConfigHelper.GetByKey("Token");
            var sessions = System.Web.HttpContext.Current.Session["Token"].ToString();

            client.BaseAddress = new Uri(Commond.ConfigHelper.GetByKey("BaseAddress"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            client.DefaultRequestHeaders.Add("Token", sessions);
            var response = await client.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<List<T>>(body);
             // var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
                return data;
            }
            throw new Exception(body);
        }
    }

    //public async Task<bool> Delete(string url)
    //{
    //    var sessions = _httpContextAccessor
    //       .HttpContext
    //       .Session
    //       .GetString(SystemConstants.AppSettings.Token);
    //    var client = _httpClientFactory.CreateClient();
    //    client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

    //    var response = await client.DeleteAsync(url);
    //    if (response.IsSuccessStatusCode)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
}
