using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticalApprouchToReplatform.New.Commands;
using PracticalApprouchToReplatform.New.Models;

namespace PracticalApprouchToReplatform.New.Controllers
{
    public class PackageController : Controller
    {
        private readonly IMicroService2AntiCorruption _microService2AntiCorruption;
        private readonly IPackageRepository _repository;

        public PackageController(IPackageRepository repository, IMicroService2AntiCorruption microService2AntiCorruption)
        {
            _microService2AntiCorruption = microService2AntiCorruption;
            _repository = repository;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePackageCommand command)
        {
            await _repository.Add(new Package(command.Barcode, command.Destination));

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