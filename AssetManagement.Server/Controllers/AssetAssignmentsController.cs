// Location: D:\AssetManagementApp\Server\Controllers\AssetAssignmentsController.cs

using Microsoft.AspNetCore.Mvc;
using AssetManagement.Data.Repositories;
using AssetManagement.Shared.Models;
using System.Collections.Generic; // Added for clarity
using System.Threading.Tasks; // Added for clarity
using System; // Added for ArgumentException/InvalidOperationException

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
        
        // ... (All endpoints remain the same, returning AssetAssignment)
        
        // NOTE: If the server crashes here, it means the repository methods
        // like _assignmentRepository.GetActiveAssignmentsAsync() are loading the 
        // full Asset entity, which you must fix by creating an AssetAssignmentDto.
    }
}