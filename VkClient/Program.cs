using System;

namespace VkClient
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    class Program
    {
        private static async Task Main(string[] args)
        {
            var httpClient = new System.Net.Http.HttpClient();

            //var authString = "https://oauth.vk.com/authorize?client_id=тут_был_клиент_id&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends&response_type=token&v=5.52";
            //authString = authString.Replace("&", "^&");
            //Process.Start(new ProcessStartInfo(
            //    "cmd",
            //    $"/c start {authString}")
            //    { CreateNoWindow = true });

            Console.Write("Input token: ");
            var accessToken = Console.ReadLine();
            Console.Write("Input id: ");
            var idString = Console.ReadLine();

            {
                var request = "https://api.vk.com/method/users.get?user_id=" + idString + "&v=5.52&access_token=" + accessToken;
                var response = await httpClient.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JObject>(content);

                    var responseData = data.Value<JToken>("response");
                    var nameData = responseData.Value<JToken>("first_name");  // not working
                }
            }

            {
                var friendsRequestString = "https://api.vk.com/method/friends.getOnline?v=5.52&access_token=" + accessToken;
                var response = await httpClient.GetAsync(friendsRequestString);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<JObject>(content);
                }
            }
        }

    }
}
