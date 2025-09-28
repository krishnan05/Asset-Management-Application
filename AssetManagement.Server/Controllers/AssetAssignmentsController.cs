using Microsoft.AspNetCore.Mvc;
using AssetManagement.Data.Repositories;
using AssetManagement.Shared.Models;

namespace AssetManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetAssignmentsController : ControllerBase
    {
        private readonly IAssetAssignmentRepository _assignmentRepository;

        public AssetAssignmentsController(IAssetAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        // GET: api/assetassignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetAssignment>>> GetAllAssignments()
        {
            var assignments = await _assignmentRepository.GetAllAssignmentsAsync();
            return Ok(assignments);
        }

        // GET: api/assetassignments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetAssignment>> GetAssignmentById(int id)
        {
            var assignment = await _assignmentRepository.GetAssignmentByIdAsync(id);
            if (assignment == null) return NotFound();
            return Ok(assignment);
        }

        // POST: api/assetassignments
        [HttpPost]
        public async Task<ActionResult> AddAssignment([FromBody] AssetAssignment assignment)
        {
            await _assignmentRepository.AddAssignmentAsync(assignment);
            return CreatedAtAction(nameof(GetAssignmentById), new { id = assignment.Id }, assignment);
        }

        // PUT: api/assetassignments/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAssignment(int id, [FromBody] AssetAssignment assignment)
        {
            if (id != assignment.Id) return BadRequest();

            await _assignmentRepository.UpdateAssignmentAsync(assignment);
            return NoContent();
        }

        // DELETE: api/assetassignments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAssignment(int id)
        {
            await _assignmentRepository.DeleteAssignmentAsync(id);
            return NoContent();
        }
    }
}
