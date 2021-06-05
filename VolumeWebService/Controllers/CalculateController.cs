using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VolumeWebService.DataAccess;
using VolumeWebService.Models;
using VolumeWebService.VolumeCalculator;

namespace VolumeWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculateController : ControllerBase
    {
        private Calculator calculator;
        private VolumeDbContext _volumeDbContext;

        public CalculateController(Calculator calculator, VolumeDbContext volumeDbContext)
        {
            this.calculator = calculator;
            _volumeDbContext = volumeDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IList<VolumeResult>>> GetAllVolumeResult()
        {
            try
            {
                IList<VolumeResult> volumes = _volumeDbContext.VolumeResults.ToList();
                return Ok(volumes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
        [HttpPost]
        [Route("cylinder")]
        public async Task<ActionResult<VolumeResult>> VolumeCylinder([FromQuery] decimal height, [FromQuery] decimal radius)
        {
            VolumeResult volumeResult = new VolumeResult
            {
                Type = "Cylinder",
                Height = height,
                Radius = radius,
                Volume = calculator.CalculateVolumeCylinder(height, radius)
            };

            _volumeDbContext.Add(volumeResult);
            await _volumeDbContext.SaveChangesAsync();
            
            return Created("/cylinder", volumeResult);
        }
        
        
        [HttpPost]
        [Route("cone")]
        public async Task<ActionResult<VolumeResult>> VolumeCone([FromQuery] decimal height, [FromQuery] decimal radius)
        {
            VolumeResult volumeResult = new VolumeResult
            {
                Type = "Cone",
                Height = height,
                Radius = radius,
                Volume = calculator.CalculateVolumeCone(height, radius)
            };
            _volumeDbContext.Add(volumeResult);
            await _volumeDbContext.SaveChangesAsync();

            return Created("/cone", volumeResult);
        }
    }
}