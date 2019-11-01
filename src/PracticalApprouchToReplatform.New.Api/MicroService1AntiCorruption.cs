using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace PracticalApprouchToReplatform.New.Api
{
    public interface IMicroService1AntiCorruption
    {
        Task<int> GetUserId();
    }

    public class MicroService1AntiCorruption : IMicroService1AntiCorruption
    {

        public Task<int> GetUserId()
        {
           return Task.FromResult(new Random().Next(0, 100000));
        }
    }
}