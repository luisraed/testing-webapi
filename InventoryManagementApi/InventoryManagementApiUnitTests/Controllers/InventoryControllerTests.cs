using InventoryManagementApi.Controllers;
using InventoryManagementApi.Repositories;
using InventoryManagementDto;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace InventoryManagementApiUnitTests.Controllers
{
    public class InventoryControllerTests
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryControllerTests()
        {
            _inventoryRepository = Substitute.For<IInventoryRepository>();
        }

        public InventoryController CreateSut()
        {
            return new InventoryController(_inventoryRepository);
        }

        [Fact]
        public async Task Get_WithExistingProductId_ReturnsResult()
        {
            var productId = 1;
            var expected = new InventoryResponse { ProductId = productId, Quantity = 2 };
            var expectedObjectResult = new OkObjectResult(expected);
            _inventoryRepository.Get(productId).Returns(expected);

            var sut = CreateSut();

            var actual = sut.Get(productId);

            actual.As<OkObjectResult>().Should().BeEquivalentTo(expectedObjectResult);
        }

        [Fact]
        public async Task Get_WithNonExistingProductId_ReturnsNotFound()
        {
            var productId = 1;
            InventoryResponse inventoryResponse = null;
            
            var expectedObjectResult = new NotFoundResult();

            _inventoryRepository.Get(productId).Returns(inventoryResponse);

            var sut = CreateSut();

            var actual = sut.Get(productId);

            actual.As<NotFoundResult>().Should().BeEquivalentTo(expectedObjectResult);
        }

        [Fact]
        public async Task Put_WithExistingProductId_ReturnsUpdatedResource()
        {
            var productId = 1;
            var quantity = 2;
            InventoryUpdateRequest request = new InventoryUpdateRequest { ProductId = productId, Quantity = quantity };
            InventoryResponse inventoryResponse = new InventoryResponse { ProductId = productId, Quantity = quantity };

            var expectedObjectResult = new OkObjectResult(inventoryResponse);

            _inventoryRepository.Update(productId, quantity).Returns(inventoryResponse);

            var sut = CreateSut();

            var actual = sut.Put(request);

            actual.As<OkObjectResult>().Should().BeEquivalentTo(expectedObjectResult);
        }

        [Fact]
        public async Task Put_WithNonExistingProductId_ReturnsNotFound()
        {
            var productId = 1;
            var quantity = 2;
            InventoryUpdateRequest request = new InventoryUpdateRequest { ProductId = productId, Quantity = quantity };
            InventoryResponse inventoryResponse = null;

            var expectedObjectResult = new NotFoundResult();

            _inventoryRepository.Update(productId, quantity).Returns(inventoryResponse);

            var sut = CreateSut();

            var actual = sut.Put(request);

            actual.As<NotFoundResult>().Should().BeEquivalentTo(expectedObjectResult);
        }

        [Fact]
        public async Task Post_WithRequest_ReturnsResult()
        {
            var productId = 1;
            var quantity = 2;
            InventoryPostRequest request = new InventoryPostRequest { Quantity = quantity };
            InventoryResponse inventoryResponse = new InventoryResponse { ProductId = productId, Quantity = quantity };

            var expectedObjectResult = new OkObjectResult(inventoryResponse);

            _inventoryRepository.Add(quantity).Returns(inventoryResponse);

            var sut = CreateSut();

            var actual = sut.Post(request);

            actual.As<OkObjectResult>().Should().BeEquivalentTo(expectedObjectResult);
        }
    }
}
