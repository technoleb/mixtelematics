using DashboardAPI.Common;
using DashboardAPI.Model;
using DashboardAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceRepository _iDistanceRepository;
        public DistanceController(IDistanceRepository iDistanceRepository)
        {
            _iDistanceRepository = iDistanceRepository;
        }

        [HttpGet("by-province")]
        public APIResponse ByProviance()
        {
            var result = _iDistanceRepository.GetDistanceByProviance();
            return result;
        }

        [HttpGet("by-site/{provianceId}")]
        public APIResponse BySite(int provianceId)
        {
            var result = _iDistanceRepository.GetDistanceBySite(provianceId);
            return result;
        }

        [HttpGet("by-driver/{siteId}")]
        public APIResponse ByDriver(int siteId)
        {
            var result = _iDistanceRepository.GetDistanceByDriver(siteId);
            return result;
        }
    }
}
