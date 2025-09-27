using Microsoft.AspNetCore.Mvc;
using AssetManagement.Data.Repositories;
using AssetManagement.Shared.Models;

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

        // GET: api/assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAllAssets()
        {
            var assets = await _assetRepository.GetAllAssetsAsync();
            return Ok(assets);
        }

        // GET: api/assets/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssetById(int id)
        {
            var asset = await _assetRepository.GetAssetByIdAsync(id);
            if (asset == null) return NotFound();
            return Ok(asset);
        }

        // POST: api/assets
        [HttpPost]
        public async Task<ActionResult> AddAsset([FromBody] Asset asset)
        {
            await _assetRepository.AddAssetAsync(asset);
            return CreatedAtAction(nameof(GetAssetById), new { id = asset.Id }, asset);
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
