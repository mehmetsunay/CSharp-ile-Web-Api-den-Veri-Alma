using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp5
{

    class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/posts";

            var data = HttpHelper.GetDataFromApi<List<Post>>(apiUrl).Result;

            Console.WriteLine(data);
            
            Console.Read();
        }

    }
    /* HttpHelper class’ının içerisinde tanımladığımız metodda 
      HttpClient ile metoda gönderilen url’den dönen JSON verisinin oluşturulan Post
      class’ına çevirmesini sağlamak için ise JsonConvert.DeserializeObject
      metodu kullanılmıştır
    
      GetDataFromApi'nın olduğu yerde ise dönüş tipim List<Post> 'tur
      Son olarak ise Api’den gelen JSON verisi Post 
      classına eşlenerek bir liste haline gelmiştir.*/

    //-https://dummyjson.com/products/1
    public class HttpHelper
    {
        public static async Task<T> GetDataFromApi<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                return resultContent;
            }
        }
    }

    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }


}
