using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection.Services;

namespace SportsStore.Controllers
{
    public class AboutController : Controller
    {
        private readonly IDetectionService _detectionService;

        public AboutController(IDetectionService detectionService)
        {
            _detectionService = detectionService;
        }

        public IActionResult Index()
        {
            return View(_detectionService);
        }
    }
}
