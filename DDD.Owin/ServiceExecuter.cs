using System;
using System.Linq;
using Castle.Core;
using Newtonsoft.Json;

namespace DDD.Owin
{
    public class ActionResult
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
    }
    public class ServiceExecuter
    {
        public static ActionResult Execute(RequestedServiceInfo info)
        {
            var result = new ActionResult();
            if (ServiceContainer.Services.Any(a => a.Name == info.Name))
            {
                var service = ServiceContainer.Services.Single(a => a.Name == info.Name).GetInstance();
                var methodInfo = service.GetType().GetMethod(info.Action);
                if (methodInfo != null)
                {
                    var jsonArray = JsonConvert.DeserializeObject<string[]>(info.Paramaters);
                    var parameterTypes = methodInfo.GetParameters().Select(a => a.ParameterType).ToArray();
                    var parameters = Enumerable.Range(0, parameterTypes.Length)
                          .Select(i => JsonConvert.DeserializeObject(jsonArray[i], parameterTypes[i])).ToArray();

                    result.Result = methodInfo.Invoke(service, parameters);
                    result.IsSuccess = true;
                    return result;
                }
            }
            throw new Exception("No action matched.");
        }
    }
}