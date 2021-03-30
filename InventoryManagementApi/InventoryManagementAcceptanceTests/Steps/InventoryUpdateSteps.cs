using InventoryManagementAcceptanceTests.Helpers;
using InventoryManagementDto;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace InventoryManagementAcceptanceTests.Steps
{
    [Binding]
    public class InventoryUpdateSteps
    {
        private InventoryUpdateRequest _request;
        private InventoryResponse _existingInventory;
        private InventoryResponse _response;
        private InventoryManagementClient _inventoryManagementClient;

        public InventoryUpdateSteps(InventoryManagementClient inventoryManagementClient)
        {
            _inventoryManagementClient = inventoryManagementClient;
        }

        [Given(@"an Inventory Update Request for an existing product")]
        public async Task GivenAnInventoryUpdateRequestForAnExistingProduct()
        {
            _existingInventory = await _inventoryManagementClient.PostAsync(new InventoryPostRequest { Quantity = 10 });
            _request = new InventoryUpdateRequest { ProductId = _existingInventory.ProductId };
        }

        [Given(@"request is to add ""(.*)"" products to the inventory")]
        public void GivenRequestIsToAddProductsToTheInventory(int productsToAdd)
        {
            _request.Quantity = productsToAdd;
        }

        [Given(@"request is to substract one product from the inventory")]
        public void GivenRequestIsToSubstractOneProductFromTheInventory()
        {
            _request.Quantity = -1;
        }

        [Given(@"request is to substract more products than the existing")]
        public void GivenRequestIsToSubstractMoreProductsThanTheExisting()
        {
            _request.Quantity = (-1)*(_existingInventory.Quantity+1);
        }

        [When(@"the Inventory Update Api is called")]
        public async Task WhenTheInventoryUpdateApiIsCalled()
        {
            _response = await _inventoryManagementClient.PutAsync(_request);
        }

        [Then(@"the quantity of products should be increased by ""(.*)""")]
        public void ThenTheQuantityOfProductsShouldBeIncreasedBy(int quantityOfProductsAdded)
        {
            Assert.Equal(_response.Quantity, _existingInventory.Quantity + quantityOfProductsAdded);
        }

        [Then(@"the quantity of products should be decreased by ""(.*)""")]
        public void ThenTheQuantityOfProductsShouldBeDecreasedBy(int quantityOfProductsSubstracted)
        {
            Assert.Equal(_response.Quantity, _existingInventory.Quantity - quantityOfProductsSubstracted);
        }

        [Then(@"the quantity of products should not change")]
        public void ThenTheQuantityOfProductsShouldNotChange()
        {
            Assert.Equal(_response.Quantity, _existingInventory.Quantity);
        }

    }
}
