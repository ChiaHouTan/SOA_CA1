namespace SOA_CA1.Service
{
    public interface IDogBreedsService
    {
        Task<DogBreed> GetDogBreedByNameAsync(string breedName);
        Task<string[]> GetRandomImagesAsync(string breed, int count = 9);
    }
}
