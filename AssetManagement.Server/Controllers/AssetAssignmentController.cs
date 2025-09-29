using Microsoft.AspNetCore.Mvc;
using AssetManagement.Data.Repositories; 
using AssetManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System; 

namespace AssetManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetAssignmentsController : ControllerBase
    {
        private readonly IAssetAssignmentRepository _assignmentRepository;
        private readonly IAssetRepository _assetRepository; 

        public AssetAssignmentsController(
            IAssetAssignmentRepository assignmentRepository,
            IAssetRepository assetRepository)
        {
            _assignmentRepository = assignmentRepository;
            _assetRepository = assetRepository;
        }

       //GET: api/AssetAssignments (Fetches all assignments)
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetAllAssignments()
        {
            var assignments = await _assignmentRepository.GetAllAssignmentsAsync();
            var assignmentDtos = assignments.Select(MapToDto).ToList();
            return Ok(assignmentDtos);
        }
        
        // GET: api/AssetAssignments/Active
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<AssetAssignmentDto>>> GetActiveAssignments()
        {
            var assignments = await _assignmentRepository.GetActiveAssignmentsAsync();
            var assignmentDtos = assignments.Select(MapToDto).ToList();
            return Ok(assignmentDtos);
        }

        // POST: api/AssetAssignments/Assign
        [HttpPost("Assign")]
        public async Task<ActionResult> AssignAsset([FromBody] AssetAssignment assignment)
        {
            if (assignment.AssetId == 0 || assignment.EmployeeId == 0)
            {
                return BadRequest("AssetId and EmployeeId are required.");
            }
            var asset = await _assetRepository.GetAssetByIdAsync(assignment.AssetId);
            if (asset == null || asset.Status != AssetStatus.Available)
            {
                return BadRequest($"Asset ID {assignment.AssetId} is not available for assignment.");
            }

            await _assignmentRepository.AddAssignmentAsync(assignment);

            asset.Status = AssetStatus.Assigned;
            await _assetRepository.UpdateAssetAsync(asset);

            return NoContent(); 
        }

        // PUT: api/AssetAssignments/Return/{assignmentId}
        [HttpPut("Return/{assignmentId}")]
        public async Task<ActionResult> ReturnAsset(int assignmentId)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(assignmentId);
            if (assignment == null) return NotFound();

            if (assignment.ReturnDate != null)
            {
                return BadRequest("Asset is already marked as returned.");
            }

            assignment.ReturnDate = DateTime.Now; 
            await _assignmentRepository.UpdateAssignmentAsync(assignment);

            var asset = await _assetRepository.GetAssetByIdAsync(assignment.AssetId);
            if (asset != null)
            {
                asset.Status = AssetStatus.Available;
                await _assetRepository.UpdateAssetAsync(asset);
            }

            return NoContent();
        }
        
        private static AssetAssignmentDto MapToDto(AssetAssignment entity) =>
            new AssetAssignmentDto
            {
                Id = entity.Id,
                AssetId = entity.AssetId,
                EmployeeId = entity.EmployeeId,
                AssignedDate = entity.AssignedDate,
                ReturnDate = entity.ReturnDate, 
                AssetName = entity.Asset?.Name ?? "Unknown Asset", 
                EmployeeName = entity.Employee?.Name ?? "Unknown Employee"
            };
    }
}