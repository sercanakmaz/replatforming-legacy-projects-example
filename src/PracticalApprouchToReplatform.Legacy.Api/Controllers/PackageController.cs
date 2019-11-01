using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticalApprouchToReplatform.Legacy.Api.Commands;
using PracticalApprouchToReplatform.Legacy.Api.Persistence;

namespace PracticalApprouchToReplatform.Legacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        private readonly IMicroService1AntiCorruption _microService1AntiCorruption;
        private readonly IMicroService2AntiCorruption _microService2AntiCorruption;
        private readonly PackageContext _context;

        public PackageController(PackageContext context, IMicroService1AntiCorruption microService1AntiCorruption, IMicroService2AntiCorruption microService2AntiCorruption)
        {
            _microService1AntiCorruption = microService1AntiCorruption;
            _microService2AntiCorruption = microService2AntiCorruption;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreatePackageCommand command)
        {
            var package = new Package(command.Barcode, command.Destination);
            _context.Packages.Add(package);
            await _context.SaveChangesAsync();
            
            AddCreatePostJob(command);

            return this.Created($"/packages/{package.Id}", package);
        }

        private void AddCreatePostJob(CreatePackageCommand command)
        {
            if (command.IsOurCommand()) 
            {
                FluentScheduler.JobManager.AddJob(
                    async () =>
                    {
                        var userId = await _microService1AntiCorruption.GetUserId();
                        await _microService2AntiCorruption.CreatePackage(new RemotePackage(command.Barcode, command.Destination, userId));
                    },
                    schedule => schedule.ToRunOnceAt(DateTime.Now.AddSeconds(1)));
            }
        }
    }
}