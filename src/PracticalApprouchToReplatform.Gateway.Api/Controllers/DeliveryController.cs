using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalApprouchToReplatform.Gateway.Api.Commands;

namespace PracticalApprouchToReplatform.Gateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly INewApiClient _newApiClient;
        private readonly ILegacyApiClient _legacyApiClient;

        public DeliveryController(INewApiClient newApiClient, ILegacyApiClient legacyApiClient)
        {
            _newApiClient = newApiClient;
            _legacyApiClient = legacyApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDeliveryCommand command)
        {
            command.Source = SourceConsts.New;
            var response = await _newApiClient.CreateDelivery(command);

            if (!response.IsSuccessStatusCode)
            {
                return this.StatusCode((int) response.StatusCode, response.Content);
            }

            AddSecondaryCallJob(command);

            return this.StatusCode((int) response.StatusCode, response.Content);
        }

        private void  AddSecondaryCallJob(CreateDeliveryCommand command)
        {
            FluentScheduler.JobManager.AddJob(
                async () =>
                {
                    await _legacyApiClient.CreatePackage(new CreatePackageCommand
                    {
                        Barcode = command.Barcode,
                        Destination = command.Destination,
                        Source = command.Source
                    });
                },
                schedule => schedule.ToRunOnceAt(DateTime.Now.AddSeconds(1)));
        }
    }
}