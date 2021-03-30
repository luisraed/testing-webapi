using InventoryManagementDto;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementApi.Repositories
{
    public interface IInventoryRepository
    {
        InventoryResponse Get(int productId);
        InventoryResponse Update(int productId, int quantity);
        InventoryResponse Add(int quantity);
    }
    public class InventoryRepository : IInventoryRepository
    {
        private List<InventoryResponse> _inventory;

        public InventoryRepository()
        {
            _inventory = new List<InventoryResponse>();
        }

        public InventoryResponse Add(int quantity)
        {
            var productId = _inventory.Count() == 0 ? 1 : _inventory.Max(x => x.ProductId) + 1;

            _inventory.Add(new InventoryResponse { ProductId = productId, Quantity = quantity });

            return Get(productId);
        }

        public InventoryResponse Get(int productId)
        {
            return _inventory.Find(i => i.ProductId == productId);
        }

        public InventoryResponse Update(int productId, int quantity)
        {
            var existingInventory = Get(productId);

            if ((existingInventory?.Quantity + quantity) < 0)
                return existingInventory;

            _inventory.Where(x => x.ProductId == productId).FirstOrDefault().Quantity += quantity;

            return Get(productId);
        }
    }
}
