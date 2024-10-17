using RestSharp;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SOA_CA1.Service
{

    public class DogBreedsService : IDogBreedsService
    {
        private static readonly string Api_url = "https://api.api-ninjas.com/v1/dogs";
        private static readonly string ApiKey = "kEYxvRU7WUwd77Gdv6zrRQ==iK3TgdqZnMER5aXu";
        private static readonly string DogImagesApiUrl = "https://dog.ceo/api/breed/";

        public async Task<DogBreed> GetDogBreedByNameAsync(string breedName)
        {
            var client = new RestClient(Api_url);
            var request = new RestRequest();

            // Add API key in the headers
            request.AddHeader("X-Api-Key", ApiKey);

            // Add the query parameter (for example: ?name=labrador)
            request.AddQueryParameter("name", breedName);

            // Execute the request asynchronously
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Deserialize the JSON response into a list of breeds
                var dogBreeds = JsonSerializer.Deserialize<List<DogBreed>>(response.Content);

                // Return the first matching breed or null if none found
                return dogBreeds != null && dogBreeds.Count > 0 ? dogBreeds[0] : null;
            }
            else
            {
                // Handle any errors (e.g., return null or an error object)
                return null;
            }
        }

        public async Task<string[]> GetRandomImagesAsync(string breed, int count = 9)
        {
            var client = new RestClient(DogImagesApiUrl);
            var request = new RestRequest($"{breed.ToLower()}/images/random/{count}", Method.Get);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = response.Content;
                var jsonResponse = JsonSerializer.Deserialize<DogImages>(content);
                return jsonResponse?.message.ToArray() ?? new string[0];
            }
            return new string[0];
        }
    }

    public class DogBreed
    {
        public string image_link { get; set; }
        public int good_with_children { get; set; }
        public int good_with_other_dogs { get; set; }
        public int shedding { get; set; }
        public int grooming { get; set; }
        public int drooling { get; set; }
        public int coat_length { get; set; }
        public int good_with_strangers { get; set; }
        public int playfulness { get; set; }
        public int protectiveness { get; set; }
        public int trainability { get; set; }
        public int energy { get; set; }
        public int barking { get; set; }
        public float min_life_expectancy { get; set; }
        public float max_life_expectancy { get; set; }
        public float max_height_male { get; set; }
        public float max_height_female { get; set; }
        public float max_weight_male { get; set; }
        public float max_weight_female { get; set; }
        public float min_height_male { get; set; }
        public float min_height_female { get; set; }
        public float min_weight_male { get; set; }
        public float min_weight_female { get; set; }
        public string name { get; set; }
    }

    public class DogImages
    {
        public string[] message { get; set; }
        public string status { get; set; }
    }


}