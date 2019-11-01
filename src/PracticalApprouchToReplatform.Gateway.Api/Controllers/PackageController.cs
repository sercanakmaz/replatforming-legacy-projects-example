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
public class PackageController : ControllerBase
{
private readonly INewApiClient _newApiClient;
private readonly ILegacyApiClient _legacyApiClient;

public PackageController(INewApiClient newApiClient, ILegacyApiClient legacyApiClient)
{
    _newApiClient = newApiClient;
    _legacyApiClient = legacyApiClient;
}

[HttpPost]
public async Task<IActionResult> Post(CreatePackageCommand command)
{
    command.Source = SourceConsts.Legacy;
    var response = await _legacyApiClient.CreatePackage(command);

    if (!response.IsSuccessStatusCode)
    {
        return this.StatusCode((int) response.StatusCode, response.Content);
    }

    AddSecondaryCallJob(command);

    return this.StatusCode((int) response.StatusCode, response.Content);
}

private void  AddSecondaryCallJob(CreatePackageCommand command)
{
    FluentScheduler.JobManager.AddJob(
        async () =>
        {
            await _newApiClient.CreateDelivery(new CreateDeliveryCommand()
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