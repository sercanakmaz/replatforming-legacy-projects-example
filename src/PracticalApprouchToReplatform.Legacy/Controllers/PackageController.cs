using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalApprouchToReplatform.Legacy.Commands;
using PracticalApprouchToReplatform.Legacy.Models;

namespace PracticalApprouchToReplatform.Legacy.Controllers
{
    public class PackageController : Controller
    {
        private readonly DefaultContext _context;

        public PackageController(DefaultContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePackageCommand input)
        {
            _context.Packages.Add(new Package
            {
                Barcode = input.Barcode
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}