using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Sample data for demonstration
            var claimDataList = new List<ClaimData>
    {
        new ClaimData
        {
            ClaimID = 1,
            ClaimantName = "John Doe",
            ClaimDate = DateTime.Now,
            ClaimStatus = "Pending",
            ClaimAmount = 1000.00m,
            LastModifiedBy = "Admin",
            LastModifiedDate = DateTime.Now
        },
        // Add more items if needed
    };

            // Return the view with the list of claim data
            return View(claimDataList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
public IActionResult GetClaimData(int claimID)
{
    // Sample data for demonstration; you should fetch data based on claimID from your data source
    var claimDataList = new List<ClaimData>
    {
        new ClaimData
        {
            ClaimID = claimID,
            ClaimantName = "John Doe",
            ClaimDate = DateTime.Now,
            ClaimStatus = "Pending",
            ClaimAmount = 1000.00m,
            LastModifiedBy = "Admin",
            LastModifiedDate = DateTime.Now
        }
        // Add logic to fetch real data based on claimID
    };

    // Return the filtered data to the view
    return View("Index", claimDataList);
}

    }
}
