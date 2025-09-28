// Location: AssetManagement.Server/Controllers/AssetsController.cs

using Microsoft.AspNetCore.Mvc;
using AssetManagement.Data.Repositories;
using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        // GET: api/assets - Returns AssetDto to break the cycle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAllAssets()
        {
            var assets = await _assetRepository.GetAllAssetsAsync();
            
            // Map Asset entity to AssetDto 
            var assetDtos = assets.Select(a => new AssetDto
            {
                Id = a.Id,
                Name = a.Name,
                SerialNumber = a.SerialNumber,
                PurchaseDate = a.PurchaseDate,
                // Convert the AssetStatus enum to its string name
                Status = a.Status.ToString() 
            }).ToList();
            
            return Ok(assetDtos);
        }

        // GET: api/assets/{id} - Returns AssetDto
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDto>> GetAssetById(int id)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();
            
            // Map Asset entity to AssetDto
            var assetDto = new AssetDto
            {
                Id = asset.Id,
                Name = asset.Name,
                SerialNumber = asset.SerialNumber,
                PurchaseDate = asset.PurchaseDate,
                Status = asset.Status.ToString()
            };
            return Ok(assetDto);
        }

        // POST: api/assets
        [HttpPost]
        public async Task<ActionResult> AddAsset([FromBody] Asset asset)
        {
            await _assetRepository.AddAssetAsync(asset);
            
            // Return AssetDto to prevent JSON cycle crash on return value
            var assetDto = new AssetDto { 
                Id = asset.Id, 
                Name = asset.Name, 
                SerialNumber = asset.SerialNumber,
                PurchaseDate = asset.PurchaseDate,
                Status = asset.Status.ToString()
            };
            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, assetDto);
        }

        // PUT: api/assets/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsset(int id, [FromBody] Asset asset)
        {
            if (id != asset.Id) return BadRequest();

            await _assetRepository.UpdateAssetAsync(asset);
            return NoContent();
        }

        // DELETE: api/assets/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsset(int id)
        {
            await _assetRepository.DeleteAssetAsync(id);
            return NoContent();
        }
    }
}