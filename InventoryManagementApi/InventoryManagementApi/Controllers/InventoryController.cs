using InventoryManagementApi.Repositories;
using InventoryManagementDto;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var inventoryResponse = _inventoryRepository.Get(id);

            if (inventoryResponse == null)
                return NotFound();

            return Ok(inventoryResponse);
        }

        [HttpPut]
        public IActionResult Put(InventoryUpdateRequest request)
        {
            var inventoryResponse = _inventoryRepository.Update(request.ProductId, request.Quantity);

            if (inventoryResponse == null)
                return NotFound();

            return Ok(inventoryResponse);
        }

        [HttpPost]
        public IActionResult Post(InventoryPostRequest request)
        {
            var inventoryResponse = _inventoryRepository.Add(request.Quantity);
            return Ok(inventoryResponse);
        }
    }
}
