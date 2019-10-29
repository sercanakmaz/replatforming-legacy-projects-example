using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticalApprouchToReplatform.Legacy.Commands;
using PracticalApprouchToReplatform.Legacy.Models;

namespace PracticalApprouchToReplatform.Legacy.Controllers
{
    public class PackageController : Controller
    {
        private readonly IMicroService2AntiCorruption _microService2AntiCorruption;
        private readonly PackageContext _context;

        public PackageController(PackageContext context, IMicroService2AntiCorruption microService2AntiCorruption)
        {
            _microService2AntiCorruption = microService2AntiCorruption;
            _context = context;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePackageCommand command)
        {
            _context.Packages.Add(new Package(command.Barcode, command.Destination));
            await _context.SaveChangesAsync();
            
            AddCreatePostJob(command);

            return RedirectToAction(nameof(Index));
        }

        private void AddCreatePostJob(CreatePackageCommand command)
        {
            if (command.IsOurCommand())
            {
                FluentScheduler.JobManager.AddJob(
                    async () => { await _microService2AntiCorruption.CreatePackage(new RemotePackage(command.Barcode, command.Destination)); },
                    schedule => schedule.ToRunOnceAt(DateTime.Now.AddSeconds(1)));
            }
        }
    }
}