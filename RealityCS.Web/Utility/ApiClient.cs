using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.Web.Utility
{
    //https://www.c-sharpcorner.com/article/consuming-web-apis-in-asp-net-core-mvc-application/
    public class ApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly string _access_Cookie;

        private Uri BaseEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseEndpoint"></param>
        public ApiClient(Uri baseEndpoint)
        {
            if (baseEndpoint == null)
            {
                throw new ArgumentNullException("baseEndpoint");
            }
            BaseEndpoint = baseEndpoint;
            _httpClient = new HttpClient();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseEndpoint"></param>
        /// <param name="access_cookie"></param>
        public ApiClient(Uri baseEndpoint, string access_cookie) : this(baseEndpoint)
        {
            _access_Cookie = access_cookie;


        }


        /// <summary>
        /// Common method for making GET calls  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string requestUrl)
        {

            addHeaders();

            var response = await _httpClient.GetAsync(CreateRequestUri(requestUrl), HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings()
            {


            });
        }

        /// <summary>
        /// Common method for making POST calls  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<Message<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<Message<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T1> PostAsync<T1, T2>(string requestUrl, T2 content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(CreateRequestUri(requestUrl), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<T1> PostAsync<T1, T2>(string requestUrl, List<T2> content)
        {
            addHeaders();
            var response = await _httpClient.PostAsync(CreateRequestUri(requestUrl), CreateHttpContent<List<T2>>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public Uri CreateRequestUri(string relativePath)
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            return uriBuilder.Uri;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public HttpContent CreateHttpContent<T>(T content)
        {
            
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            StringContent stringContent= new StringContent(json, Encoding.UTF8, "application/json");
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        /// <summary>
        /// 
        /// </summary>
        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this._access_Cookie);
        }


    }

    [DataContract]
    public class Message<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }

    }
}
