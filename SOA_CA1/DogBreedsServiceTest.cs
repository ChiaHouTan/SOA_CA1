using Moq;
using RestSharp;
using SOA_CA1.Service;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SOA_CA1
{
    public class DogBreedsServiceTest
    {
        private Mock<IRestClient> _mockRestClient;
        private DogBreedsService _dogBreedsService;

        public DogBreedsServiceTest()
        {
            SetUp();
        }

        public void SetUp()
        {
            _mockRestClient = new Mock<IRestClient>();
            _dogBreedsService = new DogBreedsService(_mockRestClient.Object); // Inject the mocked client
        }

        [Fact]
        public async Task GetDogBreedByNameAsync_ReturnsBreed_WhenSuccessful()
        {
            // Arrange
            var breedName = "labrador";
            // Act
            var result = await _dogBreedsService.GetDogBreedByNameAsync(breedName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Labrador Retriever", result.name);
        }

        [Fact]
        public async Task GetDogBreedByNameAsync_ReturnsNull_WhenApiCallFails()
        {
            // Arrange
            var breedName = "unknown";

            // Act
            var result = await _dogBreedsService.GetDogBreedByNameAsync(breedName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetRandomImagesAsync_ReturnsImages_WhenApiCallIsSuccessful()
        {
            // Arrange
            var breed = "labrador";

            // Act
            var result = await _dogBreedsService.GetRandomImagesAsync(breed);

            // Assert
            Assert.Equal(9, result.Length);
            Assert.All(result, url => Assert.Contains("labrador", url, StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task GetRandomImagesAsync_ReturnsEmptyArray_WhenApiCallFails()
        {
            // Arrange
            var breed = "unknown";

            // Act
            var result = await _dogBreedsService.GetRandomImagesAsync(breed, 9);

            // Assert
            Assert.Empty(result);
        }
    }
}