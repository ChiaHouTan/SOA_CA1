using RestSharp;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Xml.Serialization;

namespace SOA_CA1.Components.Service
{
    public class DogBreedsService
    {
        static readonly string Sanitize_url = "https://api.api-ninjas.com/v1/dogs";

        public string SanitizeText(string rawvalue)
        {
            var client = new RestClient(Sanitize_url);
            var request = new RestRequest();
            request.AddParameter("X-Api-Key", "kEYxvRU7WUwd77Gdv6zrRQ==iK3TgdqZnMER5aXu");
            var response = client.Execute(request);


            return string.Empty;

        }

        
    }

    
}
