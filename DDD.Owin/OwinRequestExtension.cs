using System;
using System.IO;
using System.Linq;
using Microsoft.Owin;

namespace DDD.Owin
{
    public static class OwinRequestExtension
    {
        public static RequestedServiceInfo ToServiceInfo(this IOwinRequest request)
        {
            var segments = request.Uri.Segments.Select(a => a.Trim('/')).ToArray();
            if (segments.Length >= 3)
            {
                return new RequestedServiceInfo
                {
                    Name = segments[1],
                    Action = segments[2],
                    Paramaters = new StreamReader(request.Body).ReadToEnd()
                };
            }
            throw new Exception("Url not match the rule,/ServiceName/ActionName");
        }
    }
}