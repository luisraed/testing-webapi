using FluentAssertions;
using InventoryManagementApi.Repositories;
using InventoryManagementDto;
using Xunit;

namespace InventoryManagementApiUnitTests.Repositories
{
    public class InventoryRepositoryTests
    {
        public InventoryRepository CreateSut()
        {
            return new InventoryRepository();
        }

        [Fact]
        public void Add_WithQuantity_ReturnsAddedInventoryResponse()
        {
            const int quantity = 2;

            var sut = CreateSut();

            var actual = sut.Add(quantity);

            actual.Quantity.Should().Be(quantity);
        }

        [Fact]
        public void Get_WithProductId_ReturnsInventoryResponse()
        {
            const int quantity = 2;

            var sut = CreateSut();

            var expected = sut.Add(quantity);

            var actual = sut.Get(expected.ProductId);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Update_QuantityCannotBeDiscounted_ReturnsExistingInventoryResponse()
        {
            const int existingQuantity = 2;
            const int quantityToDiscount = -3;

            var sut = CreateSut();

            var expected = sut.Add(existingQuantity);

            var actual = sut.Update(expected.ProductId, quantityToDiscount);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Update_QuantityCanBeDiscounted_ReturnsDiscountedInventoryResponse()
        {
            const int existingQuantity = 2;
            const int quantityToDiscount = -1;

            var sut = CreateSut();

            var addedInventory = sut.Add(existingQuantity);

            var expected = new InventoryResponse { ProductId = addedInventory.ProductId, Quantity = existingQuantity + quantityToDiscount };

            var actual = sut.Update(addedInventory.ProductId, quantityToDiscount);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
