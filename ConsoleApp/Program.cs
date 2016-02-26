using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://192.168.60.60:6026/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response = client.GetAsync("api/products?categoryID=1&subCategoryID=28&sizeCode=85").Result;
            StringBuilder result = new StringBuilder();
            if (response.IsSuccessStatusCode)
            {
                string r = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                result.Append(string.Format("Error Code:-{0}  Error Details: {1}", (int)response.StatusCode, response.ReasonPhrase));
            }        
        }
    }
}
