using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Shared.Util
{
    public static class Internet
    {
        public static async System.Threading.Tasks.Task Get(string url, string path, Action<string> success, Action<int> error)
        {
            HttpClient client = new HttpClient {BaseAddress = new Uri(url)};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                success(await response.Content.ReadAsStringAsync());
            }
            else
            {
                error((int) response.StatusCode);
            }
        }
    }
}