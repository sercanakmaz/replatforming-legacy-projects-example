using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticalApprouchToReplatform.New.Api.Commands;
using PracticalApprouchToReplatform.New.Api.Persistence;

namespace PracticalApprouchToReplatform.New.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly IMicroService2AntiCorruption _microService2AntiCorruption;
        private readonly IDeliveryRepository _repository;

        public DeliveryController(IDeliveryRepository repository, IMicroService2AntiCorruption microService2AntiCorruption)
        {
            _microService2AntiCorruption = microService2AntiCorruption;
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateDeliveryCommand command)
        {
            var delivery = new Delivery(command.Barcode, command.Destination);
            await _repository.Add(delivery);

            AddCreatePostJob(command);

            return this.Created($"/deliveries/{delivery.Id}", delivery);
        }

        private void AddCreatePostJob(CreateDeliveryCommand command)
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